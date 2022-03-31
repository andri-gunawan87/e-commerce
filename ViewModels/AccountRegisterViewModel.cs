using System.ComponentModel;
using e_commerce.Datas.Entities;

namespace e_commerce.ViewModels
{
    public class AccountRegisterViewModel
    {
        public AccountRegisterViewModel()
        {

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

        public Customer ConvertToDbModel()
        {
            return new Customer
            {
                Id = this.Id,
                Nama = this.Nama,
                NoHp = this.NoHp,
                Username = this.Username,
                Password = this.Password,
                ProfilPicture = this.ProfilPicture,
                Email = this.Email,
                IsAdmin = this.IsAdmin,
                IdAlamat = this.IdAlamat,
            };
        }
        public AccountRegisterViewModel(Customer item)
        {
            Id = item.Id;
            Nama = item.Nama;
            NoHp = item.NoHp;
            Username = item.Username;
            Password = item.Password;
            ProfilPicture = item.ProfilPicture;
            Email = item.Email;
            IsAdmin = item.IsAdmin;
            IdAlamat = item.IdAlamat;
        }
    }
}
