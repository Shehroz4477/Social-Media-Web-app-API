using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using serverSite.Data;
using serverSite.DTOs;
using serverSite.Entities;

namespace serverSite.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        public AccountController(DataContext context)
        {
            _context = context;
            
        }

        [HttpPost("register")] // Post: /api/account/register....
        public async Task<ActionResult<AppUser>> Register(RegisterDTO registerDTO){

            if(await this.IsUserExist(_context,registerDTO.UserName)) return BadRequest("User Already Exist");

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDTO.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}