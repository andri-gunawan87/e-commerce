using e_commerce.Datas.Entities;
using e_commerce.ViewModels;
namespace e_commerce.Interface
{
    public interface IkeranjangService : ICrudService<KeranjangViewModel>
    {
        Task<List<KeranjangViewModel>> GetKeranjang(int id);
    }
}
