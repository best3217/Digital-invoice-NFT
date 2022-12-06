using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nethereum.Web3;
using Newtonsoft.Json;
using Obius.DigitalInvoice.Data;
using Obius.DigitalInvoice.Services;
using RestSharp;

namespace Obius.DigitalInvoice.Shared
{
    public partial class MainLayout
    {
        public string SelectedAddress = "";
        string WalletAddress = "";
        decimal CurrentBalance;
        string Chain = "";
        public bool isConnected = false;
        [Inject]
        LocalStorageService Storage { get; set; }
        [Inject]
        IJSRuntime IJsRuntime { get; set; }
        public JSRequestService JSRequest { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            JSRequest = new JSRequestService(Storage, IJsRuntime);
            StateHasChanged();
        }
        public async void ConnectAsync()
        {
            var address = await JSRequest.GetAccountList();
            var chainId = await JSRequest.GetChainId();
            Chain = $"{chainId}";
            if (address is not null)
            {
                if (address.Length >= 1)
                {
                    isConnected = true;
                    WalletAddress = address[0];
                    SelectedAddress = $"{WalletAddress.Substring(0, 4)}...{WalletAddress.Substring(WalletAddress.Length - 5, 4).ToUpper()}";
                    await Storage.SetLocalStorage("userWalletAddress", WalletAddress);
                    await Storage.SetLocalStorage("userWalletChain", Chain);
                }
            }
        }

        public async void DisconnectAsync()
        {
            await Storage.RemoveLocalStorage("userWalletAddress");
            await Storage.RemoveLocalStorage("chain");
            isConnected = false;
            StateHasChanged();
        }
        public async Task<decimal> GetBalanceAsync()
        {
            //var balance = await web3.Eth.GetBalance.SendRequestAsync(WalletAddress);
            //CurrentBalance = Web3.Convert.FromWei(balance.Value);
            return 0;
        }
        public string GetInfuraChain()
        {
            string chainLink = "";
            if (Chain == "1")
            {
                chainLink = "https://mainnet.infura.io/v3/03c429e1a9264189a026f3c614ff32bc";
            }
            else if (Chain == "5")
            {
                chainLink = "https://goerli.infura.io/v3/03c429e1a9264189a026f3c614ff32bc";
            }
            return chainLink;
        }
        public async Task<MoralisAuthenticationResponse?> AuthenticateAsync(string accountId, string chainId)
        {
            var client = new RestClient($"https://localhost:7276/api/Authentication/{accountId}/0/{chainId}");
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("accept", "*/*");
            var response = await client.ExecutePostAsync<MoralisAuthenticationResponse>(request);
            if (response.Content != null)
            {
                var result = JsonConvert.DeserializeObject<MoralisAuthenticationResponse>(response.Content);
                return result;
            }
            return null;
        }
        public async Task VerifyAuthenticationAsync(string _signature, string _message)
        {
            //https://connect.digital-invoice.io/
            var client = new RestClient("https://localhost:7276/api/Authentication/verify/0");
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("accept", "*/*");
            request.AddHeader("Content-Type", "application/json");
            var body = new
            {
                message = _message,
                signature = _signature
            };
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = await client.ExecutePostAsync(request);

        }
    }
}
