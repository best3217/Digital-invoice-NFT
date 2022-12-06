using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Obius.Service.Dtos;
using Obius.Service.Dtos.APIDtos;
using Obius.Service.Web3.Interfaces;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Utilities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Obius.Service.Web3
{
    public class PDFGeneratorServices : IPDFGeneratorServices
    {
        public PDFGeneratorServices()
        {

        }
        public async Task<PDFGeneratorResponse?> GenerateAsync(Invoice invoice)
        {
            var client = new RestClient("http://api.digital-invoice.io/api/invoice/generate");
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("accept", "*/*");
            request.AddHeader("Authorization", "Basic b2JpdXM6cGFzc3dvcmQ=");
            request.AddHeader("Content-Type", "application/json");
            var inv = new Rootobject()
            {
                Alias = invoice.Alias,
                ContactPerson = invoice.ContactPerson,
                CustomerRefNo = invoice.CustomerRefNo,
                CustomerWallet = invoice.CustomerWallet,
                DocumentDate = invoice.DocumentDate,
                DocumentNo = invoice.DocumentNo,
                DueDate = invoice.DueDate,
                Owner = invoice.Owner,
                PaymentOption = invoice.PaymentOption,
                Remarks = invoice.Remark,
                ServiceDate = invoice.ServiceDate,
                SubTotal = invoice.NetTotal,
                TotalDiscount = invoice.TotalDiscount,
                Total = invoice.TotalFee,
                TotalTax = invoice.TotalTax
            };
            List<Dtos.APIDtos.Item> itemLst = new();
            foreach (var item in invoice.Items)
            {
                var itm = new Dtos.APIDtos.Item
                {
                    LineNumber = item.No,
                    ItemNo = item.No,
                    Description = item.ItemDesc,
                    Quantity = item.Qty,
                    UnitPrice = item.Price,
                    Discount = item.Discount,
                    DiscountSum = item.NetTotalWithoutTax,
                    Type = item.Article,
                    Tax = item.TaxPercent,
                    TaxSum = item.Tax,
                    TotalSum = item.TotalFee,
                    SubTotal = item.TotalFee,
                };
                itemLst.Add(itm);

            }
            inv.Items = itemLst;
            var result = JsonConvert.SerializeObject(inv);
            request.AddJsonBody(result);
            //request.AddParameter("application/json", result, ParameterType.RequestBody);
            var response = await client.ExecutePostAsync<PDFGeneratorResponse>(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var convertedValue = JsonConvert.DeserializeObject<PDFGeneratorResponse>(response.Content);
                return convertedValue;
            }
            return null;
        }
    }

}
