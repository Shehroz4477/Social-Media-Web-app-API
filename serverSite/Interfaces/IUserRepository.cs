using serverSite.Entities;

namespace serverSite.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<AppUser>> GetUsersAsync();
        public Task<AppUser> GetUserByIdAsync(int id);
        public Task<AppUser> GetUserByNameAsync(string userName);
        public Task<bool> SaveAllAsync();
        public void Update(AppUser user);

    }
}