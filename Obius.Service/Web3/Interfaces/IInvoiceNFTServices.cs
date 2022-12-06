using Obius.Service.Dtos;

namespace Obius.Service.Web3.Interfaces
{
    public interface IInvoiceNFTServices
    {
        Task<ContractResponse?> CreateContractAsync(string address, string to, int paymentMode, int amount, string invoiceUrl, string[] meta);
        Task<InvoiceResponse?> GetInvoiceAsync(int tokenId);
        Task<List<InvoiceResponse>> GetInvoicesCreatedByMeAsync(string address);
        Task<List<InvoiceResponse>> GetInvoicesForMeAsync(string address);
        Task<string> GetOwnerOfTokenAsync(int tokenId);
        Task PayInvoiceAsync(string address, string to, int paymentMode, string amount, string invoiceUrl, string[] meta);
    }
}