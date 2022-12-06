namespace Obius.Service.Dtos.APIDtos
{
    public class PDFGeneratorResponse
    {
        public string pdfUrl { get; set; }
        public string pdfHashId { get; set; }
        public string imageUrl { get; set; }
        public string imageHashId { get; set; }
    }

}
