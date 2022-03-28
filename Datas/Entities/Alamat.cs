using System;
using System.Collections.Generic;

namespace e_commerce.Datas.Entities
{
    public partial class Alamat
    {
        public Alamat()
        {
            Customers = new HashSet<Customer>();
            Orders = new HashSet<Order>();
            Pengirimen = new HashSet<Pengiriman>();
        }

        public int Id { get; set; }
        public string Kecamatan { get; set; } = null!;
        public string Kelurahan { get; set; } = null!;
        public string Rt { get; set; } = null!;
        public string Rw { get; set; } = null!;
        public int KodePos { get; set; }
        public string Detail { get; set; } = null!;

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Pengiriman> Pengirimen { get; set; }
    }
}
