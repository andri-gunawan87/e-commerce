using e_commerce.Datas.Entities;
using e_commerce.ViewModels;
namespace e_commerce.Interface
{
    public interface IAuthService : ICrudService<AccountRegisterViewModel>
    {
        Task<Customer> Login(string username, string password);
    }
}
