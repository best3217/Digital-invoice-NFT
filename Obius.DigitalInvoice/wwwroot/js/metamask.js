// Create server url
const myServerUrl = "https://localhost:7276";

// ABI
const ABI = [
    {
        "inputs": [
            {
                "internalType": "string",
                "name": "_prefix_url",
                "type": "string"
            },
            {
                "internalType": "address",
                "name": "_USDT_ADDRESS",
                "type": "address"
            },
            {
                "internalType": "address",
                "name": "_USDC_ADDRESS",
                "type": "address"
            },
            {
                "internalType": "address",
                "name": "_AGGREGATOR_ADDRESS",
                "type": "address"
            }
        ],
        "stateMutability": "nonpayable",
        "type": "constructor"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": true,
                "internalType": "address",
                "name": "owner",
                "type": "address"
            },
            {
                "indexed": true,
                "internalType": "address",
                "name": "approved",
                "type": "address"
            },
            {
                "indexed": true,
                "internalType": "uint256",
                "name": "tokenId",
                "type": "uint256"
            }
        ],
        "name": "Approval",
        "type": "event"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": true,
                "internalType": "address",
                "name": "owner",
                "type": "address"
            },
            {
                "indexed": true,
                "internalType": "address",
                "name": "operator",
                "type": "address"
            },
            {
                "indexed": false,
                "internalType": "bool",
                "name": "approved",
                "type": "bool"
            }
        ],
        "name": "ApprovalForAll",
        "type": "event"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": true,
                "internalType": "address",
                "name": "previousOwner",
                "type": "address"
            },
            {
                "indexed": true,
                "internalType": "address",
                "name": "newOwner",
                "type": "address"
            }
        ],
        "name": "OwnershipTransferred",
        "type": "event"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": false,
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            },
            {
                "indexed": false,
                "internalType": "enum InvoiceNFT.PAYMENT_MODE",
                "name": "",
                "type": "uint8"
            },
            {
                "indexed": false,
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            },
            {
                "indexed": false,
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "name": "SentToOwner",
        "type": "event"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": true,
                "internalType": "address",
                "name": "from",
                "type": "address"
            },
            {
                "indexed": true,
                "internalType": "address",
                "name": "to",
                "type": "address"
            },
            {
                "indexed": true,
                "internalType": "uint256",
                "name": "tokenId",
                "type": "uint256"
            }
        ],
        "name": "Transfer",
        "type": "event"
    },
    {
        "inputs": [],
        "name": "USDC_ADDRESS",
        "outputs": [
            {
                "internalType": "address",
                "name": "",
                "type": "address"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "USDT_ADDRESS",
        "outputs": [
            {
                "internalType": "address",
                "name": "",
                "type": "address"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "to",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "tokenId",
                "type": "uint256"
            }
        ],
        "name": "approve",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "owner",
                "type": "address"
            }
        ],
        "name": "balanceOf",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "uint256",
                "name": "_tokenId",
                "type": "uint256"
            }
        ],
        "name": "buildMetadata",
        "outputs": [
            {
                "internalType": "string",
                "name": "",
                "type": "string"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "_to",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "_paymentMode",
                "type": "uint256"
            },
            {
                "internalType": "uint256",
                "name": "_amount",
                "type": "uint256"
            },
            {
                "internalType": "string",
                "name": "_invoiceUrl",
                "type": "string"
            },
            {
                "internalType": "string[]",
                "name": "_meta",
                "type": "string[]"
            }
        ],
        "name": "createInvoice",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "cutPercentage",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "uint256",
                "name": "tokenId",
                "type": "uint256"
            }
        ],
        "name": "getApproved",
        "outputs": [
            {
                "internalType": "address",
                "name": "",
                "type": "address"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "getEthPrice",
        "outputs": [
            {
                "internalType": "int256",
                "name": "",
                "type": "int256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "uint256",
                "name": "_tokenId",
                "type": "uint256"
            }
        ],
        "name": "getInvoice",
        "outputs": [
            {
                "components": [
                    {
                        "internalType": "uint256",
                        "name": "tokenId",
                        "type": "uint256"
                    },
                    {
                        "internalType": "address",
                        "name": "to",
                        "type": "address"
                    },
                    {
                        "internalType": "address",
                        "name": "from",
                        "type": "address"
                    },
                    {
                        "internalType": "uint256",
                        "name": "openedOn",
                        "type": "uint256"
                    },
                    {
                        "internalType": "uint256",
                        "name": "closedOn",
                        "type": "uint256"
                    },
                    {
                        "internalType": "enum InvoiceNFT.STATUS",
                        "name": "status",
                        "type": "uint8"
                    },
                    {
                        "internalType": "enum InvoiceNFT.PAYMENT_MODE",
                        "name": "paymentMode",
                        "type": "uint8"
                    },
                    {
                        "internalType": "uint256",
                        "name": "amount",
                        "type": "uint256"
                    },
                    {
                        "internalType": "string",
                        "name": "invoiceUrl",
                        "type": "string"
                    },
                    {
                        "internalType": "uint256",
                        "name": "dummyId",
                        "type": "uint256"
                    },
                    {
                        "internalType": "uint256",
                        "name": "cut",
                        "type": "uint256"
                    },
                    {
                        "internalType": "string",
                        "name": "aliass",
                        "type": "string"
                    },
                    {
                        "internalType": "string",
                        "name": "refNo",
                        "type": "string"
                    },
                    {
                        "internalType": "string",
                        "name": "serviceDate",
                        "type": "string"
                    },
                    {
                        "internalType": "string",
                        "name": "imageUrl",
                        "type": "string"
                    }
                ],
                "internalType": "struct InvoiceNFT.INVOICE",
                "name": "",
                "type": "tuple"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "_owner",
                "type": "address"
            }
        ],
        "name": "getInvoicesCreatedByMe",
        "outputs": [
            {
                "components": [
                    {
                        "internalType": "uint256",
                        "name": "tokenId",
                        "type": "uint256"
                    },
                    {
                        "internalType": "address",
                        "name": "to",
                        "type": "address"
                    },
                    {
                        "internalType": "address",
                        "name": "from",
                        "type": "address"
                    },
                    {
                        "internalType": "uint256",
                        "name": "openedOn",
                        "type": "uint256"
                    },
                    {
                        "internalType": "uint256",
                        "name": "closedOn",
                        "type": "uint256"
                    },
                    {
                        "internalType": "enum InvoiceNFT.STATUS",
                        "name": "status",
                        "type": "uint8"
                    },
                    {
                        "internalType": "enum InvoiceNFT.PAYMENT_MODE",
                        "name": "paymentMode",
                        "type": "uint8"
                    },
                    {
                        "internalType": "uint256",
                        "name": "amount",
                        "type": "uint256"
                    },
                    {
                        "internalType": "string",
                        "name": "invoiceUrl",
                        "type": "string"
                    },
                    {
                        "internalType": "uint256",
                        "name": "dummyId",
                        "type": "uint256"
                    },
                    {
                        "internalType": "uint256",
                        "name": "cut",
                        "type": "uint256"
                    },
                    {
                        "internalType": "string",
                        "name": "aliass",
                        "type": "string"
                    },
                    {
                        "internalType": "string",
                        "name": "refNo",
                        "type": "string"
                    },
                    {
                        "internalType": "string",
                        "name": "serviceDate",
                        "type": "string"
                    },
                    {
                        "internalType": "string",
                        "name": "imageUrl",
                        "type": "string"
                    }
                ],
                "internalType": "struct InvoiceNFT.INVOICE[]",
                "name": "",
                "type": "tuple[]"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "_owner",
                "type": "address"
            }
        ],
        "name": "getInvoicesForMe",
        "outputs": [
            {
                "components": [
                    {
                        "internalType": "uint256",
                        "name": "tokenId",
                        "type": "uint256"
                    },
                    {
                        "internalType": "address",
                        "name": "to",
                        "type": "address"
                    },
                    {
                        "internalType": "address",
                        "name": "from",
                        "type": "address"
                    },
                    {
                        "internalType": "uint256",
                        "name": "openedOn",
                        "type": "uint256"
                    },
                    {
                        "internalType": "uint256",
                        "name": "closedOn",
                        "type": "uint256"
                    },
                    {
                        "internalType": "enum InvoiceNFT.STATUS",
                        "name": "status",
                        "type": "uint8"
                    },
                    {
                        "internalType": "enum InvoiceNFT.PAYMENT_MODE",
                        "name": "paymentMode",
                        "type": "uint8"
                    },
                    {
                        "internalType": "uint256",
                        "name": "amount",
                        "type": "uint256"
                    },
                    {
                        "internalType": "string",
                        "name": "invoiceUrl",
                        "type": "string"
                    },
                    {
                        "internalType": "uint256",
                        "name": "dummyId",
                        "type": "uint256"
                    },
                    {
                        "internalType": "uint256",
                        "name": "cut",
                        "type": "uint256"
                    },
                    {
                        "internalType": "string",
                        "name": "aliass",
                        "type": "string"
                    },
                    {
                        "internalType": "string",
                        "name": "refNo",
                        "type": "string"
                    },
                    {
                        "internalType": "string",
                        "name": "serviceDate",
                        "type": "string"
                    },
                    {
                        "internalType": "string",
                        "name": "imageUrl",
                        "type": "string"
                    }
                ],
                "internalType": "struct InvoiceNFT.INVOICE[]",
                "name": "",
                "type": "tuple[]"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "owner",
                "type": "address"
            },
            {
                "internalType": "address",
                "name": "operator",
                "type": "address"
            }
        ],
        "name": "isApprovedForAll",
        "outputs": [
            {
                "internalType": "bool",
                "name": "",
                "type": "bool"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "",
                "type": "address"
            }
        ],
        "name": "myTokens",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "name",
        "outputs": [
            {
                "internalType": "string",
                "name": "",
                "type": "string"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "owner",
        "outputs": [
            {
                "internalType": "address",
                "name": "",
                "type": "address"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "uint256",
                "name": "tokenId",
                "type": "uint256"
            }
        ],
        "name": "ownerOf",
        "outputs": [
            {
                "internalType": "address",
                "name": "",
                "type": "address"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "uint256",
                "name": "_tokenId",
                "type": "uint256"
            }
        ],
        "name": "payInvoice",
        "outputs": [],
        "stateMutability": "payable",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "prefixUrl",
        "outputs": [
            {
                "internalType": "string",
                "name": "",
                "type": "string"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "renounceOwnership",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "from",
                "type": "address"
            },
            {
                "internalType": "address",
                "name": "to",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "tokenId",
                "type": "uint256"
            }
        ],
        "name": "safeTransferFrom",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "from",
                "type": "address"
            },
            {
                "internalType": "address",
                "name": "to",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "tokenId",
                "type": "uint256"
            },
            {
                "internalType": "bytes",
                "name": "data",
                "type": "bytes"
            }
        ],
        "name": "safeTransferFrom",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "operator",
                "type": "address"
            },
            {
                "internalType": "bool",
                "name": "approved",
                "type": "bool"
            }
        ],
        "name": "setApprovalForAll",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "string",
                "name": "_uri",
                "type": "string"
            }
        ],
        "name": "setPrefixUrl",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "bytes4",
                "name": "interfaceId",
                "type": "bytes4"
            }
        ],
        "name": "supportsInterface",
        "outputs": [
            {
                "internalType": "bool",
                "name": "",
                "type": "bool"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "symbol",
        "outputs": [
            {
                "internalType": "string",
                "name": "",
                "type": "string"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "uint256",
                "name": "_tokenId",
                "type": "uint256"
            }
        ],
        "name": "tokenURI",
        "outputs": [
            {
                "internalType": "string",
                "name": "",
                "type": "string"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "totalSupply",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "from",
                "type": "address"
            },
            {
                "internalType": "address",
                "name": "to",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "tokenId",
                "type": "uint256"
            }
        ],
        "name": "transferFrom",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "newOwner",
                "type": "address"
            }
        ],
        "name": "transferOwnership",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    }
];

// 1. Create global userWalletAddress variable
window.userWalletAddress = null;

// 2. when the browser is ready
window.onload = async (event) => {

    // 2.1 check if ethereum extension is installed
    if (window.ethereum) {

        // 3. create web3 instance
        window.web3 = new Web3(window.ethereum);

    } else {

        // 4. prompt user to install Metamask
        alert("Please install MetaMask or any Ethereum Extension Wallet");
    }

    // 5. check if user is already logged in and update the global userWalletAddress variable
    //  window.userWalletAddress = window.localStorage.getItem("userWalletAddress");

};
window.onunload = async (event) => {
    // Clear the local storage
    localStorage.removeItem("userWalletAddress");
    localStorage.removeItem("userWalletChain");
}

window.GetEthAccounts = async () => {
    if (window.web3) {
        try {
            const accounts = await window.ethereum.request({ method: "eth_requestAccounts" });
            window.web3.eth.getAccounts()
                .then(console.log);
            return accounts;
        } catch (e) {
            console.log(e);
        }
    }
}
window.DisconnectWallet = async (account)=>
{
    if (window.web3) {
        try {
            const accounts = await window.ethereum.request({
                method: "wallet_requestPermissions", params: [{
                    eth_accounts: { account }
                }]
            });
            window.web3.eth.getAccounts()
                .then(console.log);
            return accounts;
        } catch (e) {
            console.log(e);
        }
    }
}
window.GetEthChainId = async () => {
    const chainId = await window.ethereum
        .request({
            method: "eth_chainId",
        })
        .then((chainData) => {
            return parseInt(chainData, 16);
        })
        .catch((ex) => {
            // 2.1 If the user cancels the login prompt
            throw Error(ex);
        });
    return chainId;
}
window.getBalance = async (address) => {
    const balance = await web3.eth.getBalance(address);
    return balance;
}
window.SwitchChain = async (chainName) => {
    await window.ethereum.request({
        method: 'wallet_switchEthereumChain',
        params: [{ chainId: chainName }],
    });

}
// 1. Web3 login function
window.loginWithEth = async () => {
    // 1.1 Check if there is global window.web3 instance
    if (window.web3) {
        try {
            // 2. Get the user's ethereum account - prompts metamask to login
            const selectedAccount = await window.ethereum
                .request({
                    method: "eth_requestAccounts",
                })
                .then((accounts) => accounts[0])
                .catch(() => {
                    // 2.1 If the user cancels the login prompt
                    throw Error("Please select an account");
                });

            // 3. Get the chain Id
            const chainId = await window.ethereum
                .request({
                    method: "eth_chainId",
                })
                .then((chainData) => {
                    return parseInt(chainData, 16);
                })
                .catch((ex) => {
                    // 2.1 If the user cancels the login prompt
                    throw Error(ex);
                });

            // 3. Set the global userWalletAddress variable to selected account
            window.userWalletAddress = selectedAccount;

            // 4. Store the user's wallet address in local storage
            window.localStorage.setItem("userWalletAddress", selectedAccount);
            window.localStorage.setItem("chain", chainId);

            // 5. Request signature message from serverside
            const msgRequestUrl = `${myServerUrl}/api/Authentication/${selectedAccount}/0/${chainId}`;


        } catch (error) {
            alert(error);
        }
    } else {
        alert("wallet not found");
    }
};

window.GetSignature = async (message, address) => {
    const signature = await web3.eth.sign(web3.utils.sha3(message), address);
    console.log(signature);
    return signature;
}
window.CreateContract = async (contractAddress, account, to, paymentMode, amount, invoiceUrl, meta) => {
    try {
        var contract = new web3.eth.Contract(ABI, contractAddress);
        if (contract !== null) {
            var result = await contract.methods.createInvoice(to, paymentMode, amount, invoiceUrl, meta).send({ from: account, value:web3.utils.toWei(String(0), "ether") });
            return result;
        }
        else {
            return "contract Error";
        }
    } catch (e) {
        return e.message;
    }
}
window.PayInvoice = async (contractAddress, _tokenId) => {
    try {
        var contract = new web3.eth.Contract(ABI, contractAddress);
        if (contract !== null) {
            var result = await contract.methods.payInvoice(_tokenId).send();
            return result;
        }
        else {
            return "contract Error";
        }
    } catch (e) {
        return e.message;
    }
}
window.GetInvoice = async (contractAddress,_tokenId)=>
{
    try {
        var contract = new web3.eth.Contract(ABI, contractAddress);
        if (contract !== null) {
            var result = await contract.methods.getInvoice(_tokenId).call();
            return result;
        }
        else {
            return "contract Error";
        }
    } catch (e) {
        return e.message;
    }
}
window.GetOwnerOfToken = async (contractAddress, _tokenId) => {
    try {
        var contract = new web3.eth.Contract(ABI, contractAddress);
        if (contract !== null) {
            var result = await contract.methods.ownerOf(_tokenId).call();
            return result;
        }
        else {
            return "contract Error";
        }
    } catch (e) {
        return e.message;
    }
}
window.GetInvoicesForMe = async (contractAddress, account) => {
    try {
        var contract = new web3.eth.Contract(ABI, contractAddress);
        if (contract !== null) {
            var result = await contract.methods.getInvoicesForMe(account).call();
            return result;
        }
        else {
            return "contract Error";
        }
    } catch (e) {
        return e.message;
    }
}
window.GetInvoicesCreatedByMe = async (contractAddress, account) => {
    try {
        var contract = new web3.eth.Contract(ABI, contractAddress);
        if (contract !== null) {
            var result = await contract.methods.getInvoicesCreatedByMe(account).call();
            return result;
        }
        else {
            return "contract Error";
        }
    } catch (e) {
        return e.message;
    }
}


