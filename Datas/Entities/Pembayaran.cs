using System;
using System.Collections.Generic;

namespace e_commerce.Datas.Entities
{
    public partial class Pembayaran
    {
        public int Id { get; set; }
        public string MetodePembayaran { get; set; } = null!;
        public decimal TotalBayar { get; set; }
        public int IdOrder { get; set; }
        public int IdCustomer { get; set; }
        public DateOnly Tanggal { get; set; }
        public string Tujuan { get; set; } = null!;
        public int? Pajak { get; set; }
        public string? Status { get; set; }

        public virtual Customer IdCustomerNavigation { get; set; } = null!;
        public virtual Order IdOrderNavigation { get; set; } = null!;
    }
}
