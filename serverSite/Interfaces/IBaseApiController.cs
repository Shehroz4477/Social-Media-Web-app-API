using serverSite.Data;
using serverSite.Entities;

namespace serverSite
{
    public interface IBaseApiController
    {
        public Task<bool> IsUserExist(DataContext context, string UserName);
        public Task<bool> IsUserExist(DataContext context, int Id);
        public Task<AppUser> GetUser(DataContext context, string UserName);
        public Task<AppUser> GetUser(DataContext context, int Id);
    }
}