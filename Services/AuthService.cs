using e_commerce.Datas;
using e_commerce.Datas.Entities;
using e_commerce.Interface;
using e_commerce.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace e_commerce.Services
{
    public class AuthService : BaseDbService, IAuthService
    {
        public AuthService(ecommerceContext dbContext) : base(dbContext)
        {

        }

        public async Task<Customer> Login(string username, string password)
        {
            var result = await DbContext.Customers.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
            if (result == null)
            {
                throw new Exception();
            }
            return result;
        }


        public async Task<AccountRegisterViewModel> Add(AccountRegisterViewModel obj)
        {
            // check username exist in database
            if (await DbContext.Customers.AnyAsync(x => x.Username == obj.Username))
            {
                throw new InvalidOperationException($"Username {obj.Username} sudah digunakan");
            }

            // check usernae exist in database
            if (await DbContext.Customers.AnyAsync(x => x.Email == obj.Email))
            {
                throw new InvalidOperationException($"Email {obj.Email} sudah digunakan");
            }

            // check usernae exist in database
            if (await DbContext.Customers.AnyAsync(x => x.NoHp == obj.NoHp))
            {
                throw new InvalidOperationException($"No Hp {obj.NoHp} sudah digunakan");
            }

            await DbContext.AddAsync(obj.ConvertToDbModel);
            await DbContext.SaveChangesAsync();

            return obj;
        }

        public async Task<AccountRegisterViewModel> Update(AccountRegisterViewModel obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Produk cannot be null");
            }

            var dataProduk = await DbContext.Customers.FirstOrDefaultAsync(x => x.Id == obj.Id);

            if (dataProduk == null)
            {
                throw new InvalidOperationException($"Produk with ID{obj.Id} doesnt exist in database");
            }
            dataProduk.Id = obj.Id;
            dataProduk.Nama = obj.Nama;
            dataProduk.NoHp = obj.NoHp;
            dataProduk.Username = obj.Username;
            dataProduk.Password = obj.Password;
            dataProduk.ProfilPicture = obj.ProfilPicture;
            dataProduk.Email = obj.Email;
            dataProduk.IsAdmin = obj.IsAdmin;
            dataProduk.IdAlamat = obj.IdAlamat;

            DbContext.Update(dataProduk);
            await DbContext.SaveChangesAsync();

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var dataUser = await DbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
            DbContext.Remove(dataUser);
            await DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<AccountRegisterViewModel>> Get(int limit, int offset, string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                keyword = "";
            }
            var dataInput = await DbContext.Customers.Skip(offset).Take(limit).ToListAsync();
            var result = new List<AccountRegisterViewModel>();
            foreach (var item in dataInput)
            {
                result.Add(new AccountRegisterViewModel(item));
            }
            return result;
        }

        public async Task<AccountRegisterViewModel?> Get(int id)
        {
            var result = await DbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new InvalidOperationException($"Produk dengan ID{id} tidak ada");
            };
            return new AccountRegisterViewModel(result);
        }

        public Task<AccountRegisterViewModel?> Get(Expression<Func<AccountRegisterViewModel, bool>> func)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AccountRegisterViewModel>> GetAll()
        {
            var dataInput = await DbContext.Customers.ToListAsync();
            var result = new List<AccountRegisterViewModel>();
            foreach (var item in dataInput)
            {
                result.Add(new AccountRegisterViewModel(item));
            }
            return (result);
        }
    }
}
