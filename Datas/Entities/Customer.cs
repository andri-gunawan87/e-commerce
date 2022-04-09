using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace e_commerce.Datas.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Keranjangs = new HashSet<Keranjang>();
            Orders = new HashSet<Order>();
            Pembayarans = new HashSet<Pembayaran>();
            Ulasans = new HashSet<Ulasan>();
        }

        public int Id { get; set; }
        public string Nama { get; set; } = null!;
        [DisplayName("Nomor HP")]
        public string NoHp { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        [DisplayName("Profil Picture")]
        public string? ProfilPicture { get; set; }
        public string Email { get; set; } = null!;
        [DisplayName("Apakah Admin ?")]
        public bool IsAdmin { get; set; }
        [DisplayName("Alamat")]
        public int? IdAlamat { get; set; }

        public virtual Alamat? IdAlamatNavigation { get; set; }
        public virtual ICollection<Keranjang> Keranjangs { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Pembayaran> Pembayarans { get; set; }
        public virtual ICollection<Ulasan> Ulasans { get; set; }
    }
}
