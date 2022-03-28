namespace e_commerce.ViewModels
{
    public class ProdukViewModel
    {
        public int Id { get; set; }
        public string Nama { get; set; } = null!;
        public string? Deskripsi { get; set; }
        public decimal Harga { get; set; }
        public int Stock { get; set; }
        public string? Gambar { get; set; }

    }
}
