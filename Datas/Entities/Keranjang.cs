using System;
using System.Collections.Generic;

namespace e_commerce.Datas.Entities
{
    public partial class Keranjang
    {
        public Keranjang()
        {
        }

        public int Id { get; set; }
        public int IdProduk { get; set; }
        public int IdCustomer { get; set; }
        public int JumlahBarang { get; set; }
        public decimal SubTotal { get; set; }

        public virtual Customer IdCustomerNavigation { get; set; } = null!;
        public virtual Produk IdProdukNavigation { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
