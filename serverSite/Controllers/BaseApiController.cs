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
    }
}