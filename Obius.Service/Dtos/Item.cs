namespace Obius.Service.Dtos
{
    public class Item
    {
        public int No { get; set; } = 0;
        public string Article { get; set; } = "";
        public string ItemDesc { get; set; } = "";
        public int Qty { get; set; } 
        public decimal Price { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal NetTotalWithoutTax { get; set; }
        public decimal TotalFee { get; set; }
    }
}
