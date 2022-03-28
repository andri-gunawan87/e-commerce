using System;
using System.Collections.Generic;

namespace e_commerce.Datas.Entities
{
    public partial class KategoriProduk
    {
        public int Id { get; set; }
        public int IdProduk { get; set; }
        public int IdKategori { get; set; }

        public virtual Kategori IdKategoriNavigation { get; set; } = null!;
        public virtual Produk IdProdukNavigation { get; set; } = null!;
    }
}
