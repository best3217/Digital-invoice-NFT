using Microsoft.JSInterop;
using Obius.Service.Converters;
using Obius.Service.Dtos;
using Obius.Service.Web3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Obius.Service.Web3
{
    public class InvoiceNFTServices : IInvoiceNFTServices
    {
        public readonly IJSRuntime jsRuntime;
        private readonly string contractAddress;

        public InvoiceNFTServices(IJSRuntime javaRuntime, string contractAddress)
        {
            this.jsRuntime = javaRuntime;
            this.contractAddress = contractAddress;
        }

        public async Task<ContractResponse?> CreateContractAsync(string address, string to, int paymentMode, int amount, string invoiceUrl, string[] meta)
        {
            var result = await jsRuntime.InvokeAsync<object>("CreateContract", contractAddress, address, to, paymentMode, amount, invoiceUrl, meta);
            if(result != null) 
            {
                if(!result.ToString().Contains("The requested account and/or method has not been authorized by the user."))
                {
                    var contractResult = result as ContractResponse;
                    return contractResult;
                }
            }
            return null;
        }
        public async Task PayInvoiceAsync(string address, string to, int paymentMode, string amount, string invoiceUrl, string[] meta)
        {
            var result = await jsRuntime.InvokeAsync<ContractResponse>("PayInvoice", contractAddress, address, to, paymentMode, amount, invoiceUrl, meta);
        }
        public async Task<InvoiceResponse?> GetInvoiceAsync(int tokenId)
        {
            var result = await jsRuntime.InvokeAsync<object>("GetInvoice", contractAddress, tokenId.ToString());
            if (result.GetType() == typeof(JsonElement))
            {
                var element = (JsonElement)result;
                InvoiceResponse invoice = new()
                {
                    TokenId = Convert.ToInt32(element[0].GetString()),
                    To = element[1].GetString(),
                    From = element[2].GetString(),
                    OpenedOn = TimeStampConverter.FromTimeStampToDateTime(Convert.ToInt32(element[3].GetString())),
                    ClosedOn = TimeStampConverter.FromTimeStampToDateTime(Convert.ToInt32(element[4].GetString())),
                    Status = Convert.ToInt32(element[5].GetString()),
                    PaymentMode = Convert.ToInt32(element[6].GetString()),
                    Amount = Convert.ToInt32(element[7].GetString()),
                    InvoiceUrl = element[8].GetString(),
                    DummyId = Convert.ToInt32(element[9].GetString()),
                    Cut = Convert.ToInt32(element[10].GetString()),
                    Alias = element[11].GetString(),
                    RefNo = element[12].GetString(),
                    ImageUrl = element[13].GetString()
                };
                return invoice;
            }
            return null;
        }
        public async Task<string> GetOwnerOfTokenAsync(int tokenId)
        {
            var result = await jsRuntime.InvokeAsync<string>("GetOwnerOfToken", contractAddress, tokenId.ToString());
            return result;
        }
        public async Task<List<InvoiceResponse>> GetInvoicesForMeAsync(string address)
        {
            var elements = await jsRuntime.InvokeAsync<List<JsonElement>>("GetInvoicesForMe", contractAddress, address);
            List<InvoiceResponse> invoices = new List<InvoiceResponse>();
            foreach (var element in elements)
            {
                InvoiceResponse invoice = new()
                {
                    TokenId = Convert.ToInt32(element[0].GetString()),
                    To = element[1].GetString(),
                    From = element[2].GetString(),
                    OpenedOn = TimeStampConverter.FromTimeStampToDateTime(Convert.ToInt32(element[3].GetString())),
                    ClosedOn = TimeStampConverter.FromTimeStampToDateTime(Convert.ToInt32(element[4].GetString())),
                    Status = Convert.ToInt32(element[5].GetString()),
                    PaymentMode = Convert.ToInt32(element[6].GetString()),
                    Amount = Convert.ToInt32(element[7].GetString()),
                    InvoiceUrl = element[8].GetString(),
                    DummyId = Convert.ToInt32(element[9].GetString()),
                    Cut = Convert.ToInt32(element[10].GetString()),
                    Alias = element[11].GetString(),
                    RefNo = element[12].GetString(),
                    ImageUrl = element[13].GetString()
                };
                invoices.Add(invoice);
            }
            return invoices;
        }
        public async Task<List<InvoiceResponse>> GetInvoicesCreatedByMeAsync(string address)
        {
            var elements = await jsRuntime.InvokeAsync<List<JsonElement>>("GetInvoicesCreatedByMe", contractAddress, address);
            List<InvoiceResponse> invoices = new List<InvoiceResponse>();
            foreach (var element in elements)
            {
                InvoiceResponse invoice = new()
                {
                    TokenId = Convert.ToInt32(element[0].GetString()),
                    To = element[1].GetString(),
                    From = element[2].GetString(),
                    OpenedOn = TimeStampConverter.FromTimeStampToDateTime(Convert.ToInt32(element[3].GetString())),
                    ClosedOn = TimeStampConverter.FromTimeStampToDateTime(Convert.ToInt32(element[4].GetString())),
                    Status = Convert.ToInt32(element[5].GetString()),
                    PaymentMode = Convert.ToInt32(element[6].GetString()),
                    Amount = Convert.ToInt32(element[7].GetString()),
                    InvoiceUrl = element[8].GetString(),
                    DummyId = Convert.ToInt32(element[9].GetString()),
                    Cut = Convert.ToInt32(element[10].GetString()),
                    Alias = element[11].GetString(),
                    RefNo = element[12].GetString(),
                    ImageUrl = element[13].GetString()
                };
                invoices.Add(invoice);
            }
            return invoices;
        }
    }
}




