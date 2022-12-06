using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using Obius.DigitalInvoice.Data;
using Obius.DigitalInvoice.Services;
using Obius.Service.Dtos;
using Obius.Service.Web3;
using Obius.Service.Web3.Interfaces;

namespace Obius.DigitalInvoice.Pages
{
    public partial class NewDocument
    {
        private IServiceManager serviceManager { get; set; }

        public Invoice Invoice { get; set; } = new Invoice() {Items = new() { new() { No = 1} }  };
        public Item Item { get; set; } = new Item();
        public bool IsSpinning { get; set; } = false;
        [Inject]
        private LocalStorageService Storage { get; set; }
        [Inject]
        private ISnackbar Snackbr { get; set; }
        [Inject]
        private IJSRuntime JSRequest { get; set; }
        private JSRequestService RequestService { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            RequestService = new JSRequestService(Storage,JSRequest);
           serviceManager =  new ServiceManager(JSRequest, "0xBE7d811A30BeE11e147aF76cFb53274ae9032C70");
        }
        private void PerformCalculation()
        {
            Item.NetTotalWithoutTax = Item.Qty * Item.Price;
            Item.Discount = Item.NetTotalWithoutTax * Item.DiscountPercent /100;
            Item.NetTotalWithoutTax -= Item.Discount;
            Item.Tax = Item.NetTotalWithoutTax * Item.TaxPercent / 100;
            Item.TotalFee = Item.NetTotalWithoutTax + Item.Tax;
            Invoice.TotalDiscount = 0;
            Invoice.TotalTax = 0;
            Invoice.TotalFee = 0;
            Invoice.NetTotal = 0;
            for(int i=0;i< Invoice.Items.Count;i++)
            {
                Invoice.Items[i].No = i + 1;
                Invoice.TotalDiscount += Invoice.Items[i].Discount;
                Invoice.NetTotal += Invoice.Items[i].NetTotalWithoutTax;
                Invoice.TotalTax += Invoice.Items[i].Tax;
                Invoice.TotalFee += Invoice.Items[i].TotalFee;

            }
        }
        private void NewItem()
        {
            Invoice.Items.Add(new() { No= Invoice.Items.Count+1});
        }
        private void RemoveItem()
        {
            Invoice.Items.Remove(Item);
            PerformCalculation();
        }
        private async Task CreateDocumentAsync()
        {
            IsSpinning = true;
            StateHasChanged();
            var result = Program.Configuration.GetSection("InvoiceAPI")["Username"];
            var wallet = await RequestService.GetAddressAsync();
            var pdfIPFS = await serviceManager.PDFGenerator.GenerateAsync(Invoice);
            if(pdfIPFS!= null) 
            {
              var contractResponse =  await serviceManager.InvoiceNFT.CreateContractAsync(
                wallet.Address, 
                Invoice.CustomerWallet, 
                GetPaymentMethod(), 
                GetFixedNumber(), 
                pdfIPFS.pdfUrl, 
                new string[] { Invoice.Alias, Invoice.CustomerRefNo, pdfIPFS.imageUrl,Invoice.ServiceDate.ToString() }
            );

              IsSpinning = false;
                if(contractResponse!=null)
                {
                    if(contractResponse.status)
                    {
                        Snackbr.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
                        Snackbr.Add("Contract Created!", Severity.Success);
                    }
                    else
                    {
                        Snackbr.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
                        Snackbr.Add("Transaction Failed!", Severity.Error);
                    }
                }
                else
                {
                    Snackbr.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
                    Snackbr.Add("Transaction Failed!", Severity.Error);
                }
            }
            else
            {
                Snackbr.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
                Snackbr.Add("Transaction Failed!", Severity.Error);
            }

        }

        private int GetPaymentMethod()
        {
            int result = 0;
            switch (Invoice.PaymentOption)
            {
                case "ETH":
                    result= 0;
                break;
                case "USDT":
                    result = 1;
                    break;
                case "USDC":
                    result = 2;
                    break;
                default:
                    result = 0;
                    break;
            }
            return result;
        }
        private int GetFixedNumber()
        {
            int fixe1d = 0;
            var x = Convert.ToInt32(Invoice.TotalFee * 10);
            switch (GetPaymentMethod())
            {
                case 0:
                    fixe1d = x ^ 18;
                    break;
                case 1:
                    fixe1d = x ^ 6;
                    break;
                case 2:
                    fixe1d = x ^ 6;
                    break;
                default:
                    break;
            }
            return fixe1d;
        }
        private async Task CancelTaskAsync()
        {
            IsSpinning = false;
            StateHasChanged();
        }
    }
}
