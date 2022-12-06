namespace Obius.Service.Dtos.APIDtos
{
    internal class Item
    {
        public int LineNumber { get; set; } = 0;
        public int ItemNo { get; set; }
        public string Description { get; set; } = "";
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public decimal DiscountSum { get; set; } = 0;
        public string Type { get; set; } = "";
        public decimal Tax { get; set; } = 0;
        public decimal TaxSum { get; set; } = 0;
        public decimal TotalSum { get; set; } = 0;
        public decimal SubTotal { get; set; } = 0;
    }

}
