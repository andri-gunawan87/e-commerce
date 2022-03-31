using e_commerce.Datas.Entities;
namespace e_commerce.Interface
{
    public interface IAuthService : ICrudService<Customer>
    {
        Task<Customer> Login(string username, string password);
    }
}
