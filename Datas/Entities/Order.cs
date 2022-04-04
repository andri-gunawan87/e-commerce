using System;
using System.Collections.Generic;

namespace e_commerce.Datas.Entities
{
    public partial class Order
    {
        public Order()
        {
            Pembayarans = new HashSet<Pembayaran>();
            Pengirimen = new HashSet<Pengiriman>();
            DetailOrder = new HashSet<DetailOrder>();
        }

        public int Id { get; set; }
        public DateOnly TanggalTransaksi { get; set; }
        public decimal? JumlahBayar { get; set; }
        public int IdAlamat { get; set; }
        public int IdCustomer { get; set; }
        public int IdStatus { get; set; }
        public string? Catatan { get; set; }

        public virtual Alamat IdAlamatNavigation { get; set; } = null!;
        public virtual Customer IdCustomerNavigation { get; set; } = null!;
        public virtual Keranjang IdKeranjangNavigation { get; set; } = null!;
        public virtual StatusOrder IdStatusNavigation { get; set; } = null!;
        public virtual ICollection<Pembayaran> Pembayarans { get; set; }
        public virtual ICollection<Pengiriman> Pengirimen { get; set; }
        public virtual ICollection<DetailOrder> DetailOrder { get; set; }
    }
}
