using e_commerce.Datas.Entities;
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
        public int KategoriId { get; set; }
        public string? NamaKategori { get; set; }

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
