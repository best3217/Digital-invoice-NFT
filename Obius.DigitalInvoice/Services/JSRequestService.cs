using Microsoft.JSInterop;
using Obius.DigitalInvoice.Data;

namespace Obius.DigitalInvoice.Services
{
    public class JSRequestService
    {
        public LocalStorageService _Storage { get; set; }
        public IJSRuntime IJsRuntime { get; set; }
        public JSRequestService(LocalStorageService storage, IJSRuntime iJsRuntime)
        {
            _Storage = storage;
            IJsRuntime = iJsRuntime;
        }

        public async Task<Wallet> GetAddressAsync()
        {
            var address = await _Storage.GetFromLocalStorage("userWalletAddress");
            var chainId = await _Storage.GetFromLocalStorage("userWalletChain");
            return new Wallet() { Address = address,ChainId = chainId};
        }
        public async Task<string[]> GetAccountList()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            var accounts = await IJsRuntime.InvokeAsync<string[]>("GetEthAccounts",cancellationToken: token);
            return accounts;
        }
        public async Task CopyToClipboard(string text)
        {
            await IJsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
        }
        public async Task<int> GetChainId()
        {
            var chainId = await IJsRuntime.InvokeAsync<int>("GetEthChainId");
            return chainId;
        }
        public async Task SwitchChain(string chainId)
        {
            await IJsRuntime.InvokeVoidAsync("SwitchChain", chainId);

        }
        public async Task<string> GetSignature(string message, string address)
        {
            var signature = await IJsRuntime.InvokeAsync<string>("GetSignature", message, address);
            return signature;
        }
        public async Task<object> BalanceAsync(string address)
        {
            var result = await IJsRuntime.InvokeAsync<object>("getBalance", address);
            return result;
        }
    }
}
