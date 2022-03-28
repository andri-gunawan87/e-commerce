using System;
using System.Collections.Generic;

namespace e_commerce.Datas.Entities
{
    public partial class Admin
    {
        public int Id { get; set; }
        public string Nama { get; set; } = null!;
        public string? NoHp { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? ProfilPicture { get; set; }
        public string Email { get; set; } = null!;
    }
}
