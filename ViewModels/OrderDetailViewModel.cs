namespace e_commerce.ViewModels
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public string Produk { get; set; }
        public decimal Harga { get; set; }
        public int JumlahBarang { get; set; }
        public decimal SubTotal { get; set; }
    }
}
