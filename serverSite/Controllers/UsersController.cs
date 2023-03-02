using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using serverSite.Data;
using serverSite.Entities;
using serverSite.Interfaces;

namespace serverSite.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;              
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return Ok(await _userRepository.GetUsersAsync());
        }

        [HttpGet("GetById/{Id}")]
        public async Task<ActionResult<AppUser>> GetUser(int Id){
            return await _userRepository.GetUserByIdAsync(Id);
        }

        [HttpGet("GetByName/{UserName}")]
        public async Task<ActionResult<AppUser>> GetUser(string UserName){
            return await _userRepository.GetUserByNameAsync(UserName);
        }
    }
}