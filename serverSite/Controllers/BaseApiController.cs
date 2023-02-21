using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using serverSite.Data;
using serverSite.Entities;

namespace serverSite.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // /api/controllerName
    public class BaseApiController:ControllerBase, IBaseApiController
    {
        public async Task<bool> IsUserExist(DataContext context,string UserName){
            return await context.Users.AnyAsync((AppUser User) => User.UserName == UserName.ToLower());
        }

        public async Task<bool> IsUserExist(DataContext context,int Id){
            return await context.Users.AnyAsync((AppUser User) => User.Id == Id);
        }
        public async Task<AppUser> GetUser(DataContext context,string UserName){
            return await context.Users.SingleOrDefaultAsync((AppUser User) => User.UserName == UserName.ToLower());
        }

        public async Task<AppUser> GetUser(DataContext context,int Id){
            return await context.Users.SingleOrDefaultAsync((AppUser User) => User.Id == Id);
        }
    }
}