using Microsoft.AspNetCore.Mvc;
using serverSite.Data;
using serverSite.Entities;

namespace serverSite.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // /api/users
    public class UsersController:ControllerBase
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
            
        }

        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> GetUsers()
        {
            return _context.Users.ToList();
        }

        [HttpGet("{Id}")]
        public ActionResult<AppUser> GetUser(int Id){
            return _context.Users.Find(Id);
        }
    }
}