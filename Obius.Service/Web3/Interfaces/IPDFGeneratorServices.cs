using Obius.Service.Dtos;
using Obius.Service.Dtos.APIDtos;

namespace Obius.Service.Web3.Interfaces
{
    public interface IPDFGeneratorServices
    {
        Task<PDFGeneratorResponse?> GenerateAsync(Invoice invoice);
    }
}