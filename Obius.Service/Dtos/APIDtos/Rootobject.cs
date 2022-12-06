namespace Obius.Service.Dtos.APIDtos
{
    internal class Rootobject
    {
        public string CustomerWallet { get; set; } = "";
        public int DocumentNo { get; set; }
        public DateTime DocumentDate { get; set; } = DateTime.Now;
        public string Alias { get; set; } = "";
        public string ContactPerson { get; set; } = "";
        public string CustomerRefNo { get; set; } = "";
        public DateTime ServiceDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Now;
        public string Owner { get; set; } = "";
        public string Remarks { get; set; } = "";
        public string PaymentOption { get; set; } = "";
        public decimal TotalTax { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } = "";
        public List<Item> Items { get; set; }
    }

}
