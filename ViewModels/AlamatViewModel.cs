using e_commerce.Datas.Entities;
namespace e_commerce.ViewModels
{
    public class AlamatViewModel
    {
        public AlamatViewModel()
        {

        }

        public AlamatViewModel(Alamat item)
        {
            Id = item.Id;
            Kecamatan = item.Kecamatan;
            Kelurahan = item.Kelurahan;
            Rt = item.Rt;
            Rw = item.Rw;
            KodePos = item.KodePos;
            Detail = item.Detail;
            IdUser = item.IdUser;
       }

        public int Id { get; set; }
        public string Kecamatan { get; set; } = null!;
        public string Kelurahan { get; set; } = null!;
        public string Rt { get; set; } = null!;
        public string Rw { get; set; } = null!;
        public int KodePos { get; set; }
        public string Detail { get; set; } = null!;
        public int? IdUser { get; set; }

        public Alamat ConvertToDbModel()
        {
            return new Alamat
            {
                Kecamatan = this.Kecamatan,
                Kelurahan = this.Kelurahan,
                Rt = this.Rt,
                Rw = this.Rw,
                KodePos = this.KodePos,
                Detail = this.Detail,
                IdUser = this.IdUser,

            };
        }
    }
}
