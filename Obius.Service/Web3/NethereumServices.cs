using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obius.Service.Web3
{
    internal sealed class NethereumServices
    {
        private readonly IWeb3 web3;

        public NethereumServices(IWeb3 web3)
        {
            this.web3 = web3;
        }
        public async Task<decimal> GetBalanceAsync(string walletAddress)
        {
            var balance = await web3.Eth.GetBalance.SendRequestAsync(walletAddress);
            var currentBalance = Nethereum.Web3.Web3.Convert.FromWei(balance.Value);
            return currentBalance;
        }

    }
}
