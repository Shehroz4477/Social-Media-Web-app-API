using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<AppUser>> GetUser(int Id){
            return await _context.Users.FindAsync(Id);
        }
    }
}