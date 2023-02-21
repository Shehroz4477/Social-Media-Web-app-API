using serverSite.Data;

namespace serverSite
{
    public interface IBaseApiController
    {
        public Task<bool> IsUserExist(DataContext context, string UserName);
        public Task<bool> IsUserExist(DataContext context, int Id);
    }
}