using e_commerce.Datas;
using e_commerce.Datas.Entities;
using e_commerce.Interface;
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
            return result;
        }


        public async Task<Customer> Add(Customer obj)
        {
            if (await DbContext.Produks.AnyAsync(x => x.Id == obj.Id))
            {
                throw new InvalidOperationException($"Produk with ID {obj.Id} is already exist");
            }

            await DbContext.AddAsync(obj);
            await DbContext.SaveChangesAsync();

            return obj;
        }

        public async Task<Customer> Update(Customer obj)
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

        public async Task<List<Customer>> Get(int limit, int offset, string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                keyword = "";
            }

            return await DbContext.Customers.Skip(offset).Take(limit).ToListAsync();
        }

        public async Task<Customer?> Get(int id)
        {
            var result = await DbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new InvalidOperationException($"Produk dengan ID{id} tidak ada");
            };
            return result;
        }

        public Task<Customer?> Get(Expression<Func<Customer, bool>> func)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Customer>> GetAll()
        {
            return await DbContext.Customers.ToListAsync();
        }
    }
}
