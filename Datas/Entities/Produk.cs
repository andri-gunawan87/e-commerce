using System;
using System.Collections.Generic;
using e_commerce.ViewModels;

namespace e_commerce.Datas.Entities
{
    public partial class Produk
    {
        public Produk()
        {
            KategoriProduks = new HashSet<KategoriProduk>();
            Keranjangs = new HashSet<Keranjang>();
        }

        public int Id { get; set; }
        public string Nama { get; set; } = null!;
        public string? Deskripsi { get; set; }
        public decimal Harga { get; set; }
        public int Stock { get; set; }
        public string? Gambar { get; set; }

        public virtual ICollection<KategoriProduk> KategoriProduks { get; set; }
        public virtual ICollection<Keranjang> Keranjangs { get; set; }

        public ProdukViewModel ConvertToViewModel()
        {
            return new ProdukViewModel
            {
                Id = this.Id,
                Nama = this.Nama,
                Deskripsi = this.Deskripsi,
                Harga = this.Harga,
                Stock = this.Stock,
                Gambar = this.Gambar
            };
        }
    }
}
