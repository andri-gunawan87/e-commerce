namespace e_commerce.ViewModels
{
    public class CheckoutViewModel
    {
        public int[] IdProduk { get; set; }
        public int[] JumlahBarang { get; set; }
        public int IdAlamat { get; set; }
        public string Action { get; set; }
        public string? Note { get; set; }
    }
}
