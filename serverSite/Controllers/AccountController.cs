using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using serverSite.Data;
using serverSite.DTOs;
using serverSite.Entities;
using serverSite.Interfaces;

namespace serverSite.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")] // Post: /api/account/register....
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {

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

            return new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await this.GetUser(_context, loginDTO.UserName);
            if(!this._UserAuthorized(user,loginDTO)) return Unauthorized("Invalid User Name or Password");
            return new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        // private methods
        private bool _UserAuthorized(AppUser user, LoginDTO loginDTO){
            if(user == null) return false;
            var hmac = new HMACSHA512(user.PasswordSalt);
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if(passwordHash[i] != user.PasswordHash[i])
                {
                    return false;
                }
            }
            return true;
        } 
    }
}