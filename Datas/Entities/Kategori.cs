using System;
using System.Collections.Generic;

namespace e_commerce.Datas.Entities
{
    public partial class Kategori
    {
        public Kategori()
        {
            KategoriProduks = new HashSet<KategoriProduk>();
        }

        public int Id { get; set; }
        public string Nama { get; set; } = null!;
        public string Deskripsi { get; set; } = null!;
        public string Icon { get; set; } = null!;

        public virtual ICollection<KategoriProduk> KategoriProduks { get; set; }
    }
}
