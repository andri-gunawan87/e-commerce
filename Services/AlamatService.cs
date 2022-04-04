using e_commerce.Datas;
using e_commerce.Datas.Entities;
using e_commerce.Interface;
using e_commerce.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;


namespace e_commerce.Services
{
    public class AlamatService : BaseDbService, IAlamatService
    {
        public AlamatService(ecommerceContext dbContext) : base(dbContext)
        {
        }

        public async Task<AlamatViewModel> Add(AlamatViewModel obj)
        {
            obj.Id = 0;
            var dataAlamat = obj.ConvertToDbModel();
            await DbContext.AddAsync(dataAlamat);
            await DbContext.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> Delete(int id)
        {
            var alamat = await DbContext.Alamats.FindAsync(id);
            DbContext.Alamats.Remove(alamat);
            await DbContext.SaveChangesAsync();

            return true;
        }

        public Task<List<AlamatViewModel>> Get(int limit, int offset, string keyword)
        {
            throw new NotImplementedException();
        }

        public async Task<AlamatViewModel?> Get(int id)
        {
            var result = await DbContext.Alamats
                   .FirstOrDefaultAsync(m => m.Id == id);
            var dataViewModel = new AlamatViewModel(result);
            return dataViewModel;
        }

        public Task<AlamatViewModel?> Get(Expression<Func<AlamatViewModel, bool>> func)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AlamatViewModel>> GetAll()
        {
            var result = await DbContext.Alamats.ToListAsync();
            var dataViewModel = new List<AlamatViewModel>();
            foreach (Alamat item in result)
            {
                dataViewModel.Add(new AlamatViewModel(item));
            }

            return dataViewModel;

        }

        public async Task<List<AlamatViewModel>> GetUserAlamat(int id)
        {
            var result = await DbContext.Alamats.Where(x => x.IdUser == id).ToListAsync();
            var dataViewModel = new List<AlamatViewModel>();
            foreach (Alamat item in result)
            {
                dataViewModel.Add(new AlamatViewModel(item));
            }

            return dataViewModel;
        }

        public Task<AlamatViewModel> Update(AlamatViewModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
