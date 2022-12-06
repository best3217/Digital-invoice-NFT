using NUnit.Framework;
using Obius.Service.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obius.Service.Tests
{
    [TestFixture]
    public class PDFGeneratorServicesTests
    {
        [Test]
        public async Task Return_URL_GenerateAsync()
        {
            var service = new PDFGeneratorServices();
            var data = new Dtos.Invoice() 
            {
                Alias = "abc",
                ContactPerson = "string",
                TotalDiscount = 1,
                CustomerRefNo = "as",
                CustomerWallet = "as",
                DocumentDate = DateTime.Now,
                DocumentNo = 12,
                DueDate = DateTime.Now,
                NetTotal = 12,
                Owner = "asd",
                PaymentOption = "etc",
                Remark = "asd",
                ServiceDate = DateTime.Now,
                Status = "asd",
                TotalFee = 21,
                TotalTax = 2131,
                
                Items = new List<Dtos.Item>() { 
                    new() 
                    {
                        Article = "asd",
                        TotalFee =21,
                        Discount = 1,
                        DiscountPercent = 1,
                        ItemDesc = "aasd",
                        NetTotalWithoutTax = 1,
                        No = 1 ,
                        Price = 1 ,
                        Qty = 1 ,
                        Tax = 1 ,
                        TaxPercent = 1 
                    } 
                } 
            };
            var result = await service.GenerateAsync(data);
            Assert.That(result, Is.Not.Null);
        }
    }
}
