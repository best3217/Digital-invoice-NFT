namespace Obius.Service.Dtos
{
    public class InvoiceResponse
    {
        public int TokenId { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public DateTime? OpenedOn { get; set; }
        public DateTime? ClosedOn { get; set; }
        public int Status { get; set; }
        public int PaymentMode { get; set; }
        public decimal Amount { get; set; }
        public string InvoiceUrl { get; set; }
        public int DummyId { get; set; }
        public int Cut { get; set; }
        public string Alias { get; set; }
        public string RefNo { get; set; }
        public string ImageUrl { get; set; }
    }
}




