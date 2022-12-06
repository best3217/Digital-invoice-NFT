using Microsoft.JSInterop;
using Obius.Service.Web3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obius.Service.Web3
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IInvoiceNFTServices> _lazyInvoiceNFTServices;
        private readonly Lazy<IPDFGeneratorServices> _lazyPdfGeneratorServices;

        public ServiceManager(IJSRuntime jSRuntime, string contractAddress)
        {
            _lazyInvoiceNFTServices = new Lazy<IInvoiceNFTServices>(() => new InvoiceNFTServices(jSRuntime, contractAddress));
            _lazyPdfGeneratorServices = new Lazy<IPDFGeneratorServices>(() => new PDFGeneratorServices());
        }
        public IInvoiceNFTServices InvoiceNFT => _lazyInvoiceNFTServices.Value;
        public IPDFGeneratorServices PDFGenerator => _lazyPdfGeneratorServices.Value;
    }
}
