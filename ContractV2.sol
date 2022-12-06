// SPDX-License-Identifier: MIT
/**
                                                     

 ______                                __                            __    __  ________  ________ 
/      |                              /  |                          /  \  /  |/        |/        |
$$$$$$/  _______   __     __  ______  $$/   _______   ______        $$  \ $$ |$$$$$$$$/ $$$$$$$$/ 
  $$ |  /       \ /  \   /  |/      \ /  | /       | /      \       $$$  \$$ |$$ |__       $$ |   
  $$ |  $$$$$$$  |$$  \ /$$//$$$$$$  |$$ |/$$$$$$$/ /$$$$$$  |      $$$$  $$ |$$    |      $$ |   
  $$ |  $$ |  $$ | $$  /$$/ $$ |  $$ |$$ |$$ |      $$    $$ |      $$ $$ $$ |$$$$$/       $$ |   
 _$$ |_ $$ |  $$ |  $$ $$/  $$ \__$$ |$$ |$$ \_____ $$$$$$$$/       $$ |$$$$ |$$ |         $$ |   
/ $$   |$$ |  $$ |   $$$/   $$    $$/ $$ |$$       |$$       |      $$ | $$$ |$$ |         $$ |   
$$$$$$/ $$/   $$/     $/     $$$$$$/  $$/  $$$$$$$/  $$$$$$$/       $$/   $$/ $$/          $$/    
                                                                                                  
                                                                                                                                                                                                                                        

*/

pragma solidity ^0.8.0;
//TODO RESOLVE BUG IN getAmount()

import "@openzeppelin/contracts/token/ERC721/ERC721.sol";
import "@openzeppelin/contracts/token/ERC20/IERC20.sol";
import "@openzeppelin/contracts/utils/Counters.sol";
import "@openzeppelin/contracts/access/Ownable.sol";
import "@chainlink/contracts/src/v0.8/interfaces/AggregatorV3Interface.sol";
import "./Base64.sol";

/// @title Contract to mint invoices as NFT
/// @author Cryptospaceadvi
/// @notice Using the contract the invoice can be minted to the person who owes a due and the invoice can be paid in specified payment mode only
contract InvoiceNFT is ERC721, Ownable {
    using Counters for Counters.Counter;
    using Strings for uint;
    Counters.Counter private _tokenIds;
    AggregatorV3Interface internal priceFeed;

    enum STATUS{OPEN,CLOSED}
    enum PAYMENT_MODE{ETH,USDT,USDC}
    
    address public USDT_ADDRESS;
    address public USDC_ADDRESS;
    uint public cutPercentage=1;
    string public prefixUrl;
    IERC20 tokenUSDT;
    IERC20 tokenUSDC;
    
    struct INVOICE{
        uint tokenId;
        address to;
        address from;
        uint openedOn;
        uint closedOn;
        STATUS status;
        PAYMENT_MODE paymentMode;
        uint amount;
        string invoiceUrl; //only hash is stored
        uint dummyId; //doc num
        uint cut;
        
        string aliass;
        string refNo;
        string serviceDate;
        string imageUrl; //only hash is stored

    }

    mapping(address=>uint[]) createdByMe;
    mapping(address=>uint[]) createdForMe;
    mapping(uint=>INVOICE) allInvoices;
    mapping(address=>uint) public myTokens;

    event SentToOwner(uint,PAYMENT_MODE,uint,uint);



    /// @param _prefix_url the prefix link of the pinata ipfs dedicated gateway
    /// @param _USDT_ADDRESS the address of USDT contract on the ETH chain
    /// @param _USDC_ADDRESS the address of USDC contract on the ETH chain
    /// @notice you can change the NFT name and symbol before deployment
    constructor(string memory _prefix_url,address _USDT_ADDRESS, address _USDC_ADDRESS, address _AGGREGATOR_ADDRESS) ERC721("Invoice", "INV") {
        
        prefixUrl = _prefix_url;
        USDT_ADDRESS = _USDT_ADDRESS;
        USDC_ADDRESS = _USDC_ADDRESS;
        tokenUSDT = IERC20(address(_USDT_ADDRESS));
        tokenUSDC = IERC20(address(_USDC_ADDRESS));
         priceFeed = AggregatorV3Interface(_AGGREGATOR_ADDRESS);
    }

    
    /// @param _to the address to which the NFT gets minted
    /// @param _paymentMode the mode of payment 0:ETH, 1:USDT, 2:USDC
    /// @param _amount the amoount should be specifed in the smallest unit of currency cosiderig the proper decimals
    /// @param _invoiceUrl the url of the invoice pdf on IPFS
    /// @return the minted token ID
    
    function createInvoice(address _to, uint _paymentMode, uint _amount, string memory _invoiceUrl, string[] memory _meta) public returns(uint){
       //TODO:check for valid string url is done
       bytes memory lengthTest = bytes(_invoiceUrl); // Uses memory
       require(lengthTest.length>0,"Invalid invoice url");
       require(_amount>100,"Invalid amount");
       require(_to!=msg.sender,"Cannot mint invoice to yourself");
       PAYMENT_MODE mode = PAYMENT_MODE.ETH;
       if(_paymentMode==0){
           mode = PAYMENT_MODE.ETH;
       }else if(_paymentMode==1){
           mode = PAYMENT_MODE.USDT;
       }else if(_paymentMode==2){
           mode = PAYMENT_MODE.USDC;
       }else{
           revert("Invalid payment mode");
       }
        _tokenIds.increment();
        uint256 newItemId = _tokenIds.current();
        _mint(_to, newItemId);
        createdByMe[msg.sender].push(newItemId);
        createdForMe[_to].push(newItemId);
        uint _did = myTokens[msg.sender]+1;
        myTokens[msg.sender] = myTokens[msg.sender]+1;
        
        uint _cut = (_amount*cutPercentage)/100;
        
        INVOICE memory inv = INVOICE(newItemId,_to,msg.sender,block.timestamp,0,STATUS.OPEN,mode,_amount,_invoiceUrl,_did,_cut,_meta[0],_meta[1],_meta[2],_meta[3]);
        allInvoices[newItemId]=inv;
        return newItemId;
    }


    /// @param _tokenId the token ID to be paid
    /// @notice the payer need to approve for specified token amounts if the payment mode is USDC or USDT
    function payInvoice(uint _tokenId) public payable {
        INVOICE storage inv = allInvoices[_tokenId];
        require(msg.sender==inv.to, "This invoice is not generated for you");
        require(inv.status==STATUS.OPEN,"Invoice already closed");
        inv.closedOn = block.timestamp;
        inv.status = STATUS.CLOSED;

        if(inv.paymentMode == PAYMENT_MODE.ETH){
            
            
                require(msg.value>=inv.amount,"Not enough amount sent");
                (bool success, bytes memory data) = inv.from.call{value: inv.amount-inv.cut}("");
                (bool ownSuccess, bytes memory ownData) = payable(owner()).call{value: msg.value-(inv.amount-inv.cut)}("");
                require(success, "Failed to send Ether");
                require(ownSuccess, "Failed to send Ether to owner");
                emit SentToOwner(inv.tokenId, inv.paymentMode, inv.amount, inv.cut); //tokenId,mode,amt,cut     
            
        }else if(inv.paymentMode == PAYMENT_MODE.USDT){
            
            
                bool success = tokenUSDT.transferFrom(msg.sender, inv.from, inv.amount-inv.cut);
                bool ownSuccess = tokenUSDT.transferFrom(msg.sender, owner(), inv.cut);
                require(success, "Failed to send USDT");
                require(ownSuccess, "Failed to send USDT to owner");
                emit SentToOwner(inv.tokenId, inv.paymentMode, inv.amount, inv.cut); //tokenId,mode,amt,cut     
        
        }else if(inv.paymentMode == PAYMENT_MODE.USDC){
            
                bool success = tokenUSDC.transferFrom(msg.sender, inv.from, inv.amount-inv.cut);
                bool ownSuccess = tokenUSDC.transferFrom(msg.sender, owner(), inv.cut);
                require(success, "Failed to send USDC");
                require(ownSuccess, "Failed to send USDC to owner");
                emit SentToOwner(inv.tokenId, inv.paymentMode, inv.amount, inv.cut); //tokenId,mode,amt,cut     
            
        }else{
            revert("Invalid payment mode");
        }

    }


    /// @return the invoice data [struct]
    function getInvoice(uint _tokenId) public view returns(INVOICE memory){
        require(_exists(_tokenId),"Invoice does not exist");
        return allInvoices[_tokenId];
    }

    /// @return USD price with 8 decimals
    function getEthPrice() public view returns (int) {
        (
            uint80 roundID, 
            int price,
            uint startedAt,
            uint timeStamp,
            uint80 answeredInRound
        ) = priceFeed.latestRoundData();
        return price;
    }


    /// @notice transfers are blocked
    function transferFrom(
        address from,
        address to,
        uint256 tokenId
    ) public override {
        revert("Invoices are non-transferrable");
    }


    /// @notice transfers are blocked
    function safeTransferFrom(
        address from,
        address to,
        uint256 tokenId
    ) public virtual override {
        revert("Invoices are non-transferrable");
    }


    /// @notice transfers are blocked
    function safeTransferFrom(
        address from,
        address to,
        uint256 tokenId,
        bytes memory data
    ) public override {
        revert("Invoices are non-transferrable");
    }


    /// @notice to update common uri displayed on opensea
    /// @notice only contract owner can update this
    /// @param _uri new uri 
    function setPrefixUrl(string memory _uri) external onlyOwner{ 
        prefixUrl = _uri;
    }


    /// @notice this provides common uri for opensea
     function tokenURI(uint _tokenId) public view virtual override returns (string memory) {
        require(_exists(_tokenId),"ERC721Metadata: URI query for nonexistent token");
        return buildMetadata(_tokenId);
    }


    /// @return all the invoices generated for my address
    function getInvoicesForMe(address _owner) public view returns(INVOICE[] memory){
        INVOICE[] memory temp = new INVOICE[](createdForMe[_owner].length);
        for(uint i=0;i<createdForMe[_owner].length;i++){
            temp[i]=allInvoices[createdForMe[_owner][i]];
        }
        return temp;

    }


    /// @return all the invoices generated by me
    function getInvoicesCreatedByMe(address _owner) public view returns(INVOICE[] memory){
        INVOICE[] memory temp = new INVOICE[](createdByMe[_owner].length);
        for(uint i=0;i<createdByMe[_owner].length;i++){
            temp[i]=allInvoices[createdByMe[_owner][i]];
        }
        return temp;

    }


    /// @return the total invoices generated
    function totalSupply() public view returns(uint){
        return _tokenIds.current();

    }

    

    /// @notice builds metadata
    function buildMetadata(uint _tokenId) public view returns(string memory) {
      
      INVOICE memory _invoice = allInvoices[_tokenId];
      string memory _status = "Open";
      if(_invoice.status==STATUS.CLOSED){
          _status = "Closed";
      }
      string memory _paymentMode = "ETH";
      if(_invoice.paymentMode==PAYMENT_MODE.USDT){
          _paymentMode = "USDT";
      }
      if(_invoice.paymentMode==PAYMENT_MODE.USDC){
          _paymentMode = "USDC";
      }
        
      string memory s1 = string(abi.encodePacked('{"name":"', 
                          "Invoice NFT #",_invoice.dummyId.toString(),
                          '", "description":"', 
                          "Digital invoice by Obius",
                          '", "image": "',
                          prefixUrl, 
                          _invoice.imageUrl,
                          '"'));
       string memory a1 = string(abi.encodePacked('{',
                          '"trait_type":',
                          '"Receiver Wallet"',
                          ',',
                          '"value":"',
                          toAsciiString(_invoice.to),
                          '"},',
                          '{',
                          '"trait_type":',
                          '"Alias"',
                          ',',
                          '"value":"',
                          _invoice.aliass,
                          '"},',
                          '{',
                          '"trait_type":',
                          '"Ref No"',
                          ',',
                          '"value":"',
                          _invoice.refNo,
                          '"},'
                          )
                          );           
      string memory a2 = string(abi.encodePacked('{',
                          '"trait_type":',
                          '"Doc Num"',
                          ',',
                          '"value":"',
                          _invoice.dummyId.toString(),
                          '"},',
                          '{',
                          '"trait_type":',
                          '"Status"',
                          ',',
                          '"value":"',
                          _status,
                          '"},',
                          '{',
                          '"trait_type":',
                          '"Service Date"',
                          ',',
                          '"value":"',
                          _invoice.serviceDate,
                          '"},'));            
       string memory a3 = string(abi.encodePacked('{',
                          '"trait_type":',
                          '"Payment option"',
                          ',',
                          '"value":"',
                          _paymentMode,
                          '"},',
                          '{',
                          '"trait_type":',
                          '"Amount"',
                          ',',
                          '"value":"',
                        getTotalAmount(_invoice),
                          '"}'
                          ));                                               
      return string(abi.encodePacked(
              'data:application/json;base64,', Base64.encode(bytes(abi.encodePacked(
                          s1,
                          ',',
                          '"attributes":[',
                          a1,
                          a2,
                          a3,
                          ']',
                          '}'
                          )))));

    }

    
        
    function getTotalAmount(INVOICE memory _invoice) internal view returns(string memory){
        
        uint a = _invoice.amount/10**18;
        uint b = _invoice.amount%10**18;
        string memory s = "";
        if(b>=0.1 ether && b<1 ether){
            s = string(abi.encodePacked(a.toString(),".",b.toString()));
        }else if(b>=0.01 ether && b<0.1 ether){
            s = string(abi.encodePacked(a.toString(),".","0",b.toString()));
        }else if(b>=0.001 ether && b<0.01 ether){
            s = string(abi.encodePacked(a.toString(),".","00",b.toString()));
        }else{
            s = a.toString();
        }
        
        return s;
    }
    


    function toAsciiString(address x) internal pure returns (string memory) {
        bytes memory s = new bytes(40);
        for (uint i = 0; i < 20; i++) {
            bytes1 b = bytes1(uint8(uint(uint160(x)) / (2**(8*(19 - i)))));
            bytes1 hi = bytes1(uint8(b) / 16);
            bytes1 lo = bytes1(uint8(b) - 16 * uint8(hi));
            s[2*i] = char(hi);
            s[2*i+1] = char(lo);            
        }
        return string(s);
    }


    function char(bytes1 b) internal pure returns (bytes1 c) {
        if (uint8(b) < 10) return bytes1(uint8(b) + 0x30);
        else return bytes1(uint8(b) + 0x57);
    }

}