namespace Obius.Service.Dtos
{
    public class Invoice
    {
        public string CustomerWallet { get; set; } = "";
        public string Alias { get; set; } = "";
        public string ContactPerson { get; set; } = "";
        public string CustomerRefNo { get; set; } = "";
        public int DocumentNo { get; set; } = 0;
        public string Status { get; set; } = "Open";
        public DateTime DocumentDate { get; set; } = DateTime.Now;
        public DateTime ServiceDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Now;
        public string PaymentOption { get; set; } = "ETH";
        public string Owner { get; set; } = "";
        public string Remark { get; set; } = "";
        public decimal NetTotal { get; set; } = 0;
        public decimal TotalDiscount { get; set; } = 0;
        public decimal TotalTax { get; set; } = 0;
        public decimal TotalFee { get; set; } = 0;
        public List<Item> Items { get; set; } = new List<Item>();
    }
}
