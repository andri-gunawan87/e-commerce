using e_commerce.ViewModels;

namespace e_commerce.Interface
{
    public interface IAlamatService : ICrudService<AlamatViewModel>
    {
        public Task<List<AlamatViewModel>> GetUserAlamat(int id);
    }
}
