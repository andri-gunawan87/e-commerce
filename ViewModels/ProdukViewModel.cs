using e_commerce.Datas.Entities;
namespace e_commerce.ViewModels
{
    public class ProdukViewModel
    {
        public ProdukViewModel()
        {
            Kategories = new List<KategoriViewModel>();
        }
        public ProdukViewModel(Produk item)
        {
            Id = item.Id;
            Nama = item.Nama;
            Deskripsi = item.Deskripsi;
            Harga = item.Harga;
            Stock = item.Stock;
            Gambar = item.Gambar;

        }
        public ProdukViewModel(int id, string nama, string deskripsi, decimal harga)
        {
            Id = id;
            Nama = nama;
            Deskripsi = deskripsi;
            Harga = harga;
            Stock = 100;
            KategoriId = Array.Empty<int>();
            Kategories = new List<KategoriViewModel>();
        }
        public int Id { get; set; }
        public string Nama { get; set; } = null!;
        public string? Deskripsi { get; set; }
        public decimal Harga { get; set; }
        public int Stock { get; set; }
        public string? Gambar { get; set; }
        public string GambarSrc
        {
            get
            {
                return (string.IsNullOrEmpty(Gambar) ? "images/default.png" : Gambar);
            }
        }
        public IFormFile? GambarFile { get; set; }

        public int[] KategoriId { get; set; }
        public List<KategoriViewModel> Kategories { get; set; }
        public Produk ConvertToDbModel()
        {
            return new Produk
            {
                Id = this.Id,
                Nama = this.Nama,
                Deskripsi = this.Deskripsi,
                Harga = this.Harga,
                Stock = this.Stock,
                Gambar = this.Gambar,
            };
        }
    }
}
