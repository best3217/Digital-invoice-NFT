namespace Obius.Service.Web3.Interfaces
{
    public interface IServiceManager
    {
        IInvoiceNFTServices InvoiceNFT { get; }
        IPDFGeneratorServices PDFGenerator { get; }
    }
}