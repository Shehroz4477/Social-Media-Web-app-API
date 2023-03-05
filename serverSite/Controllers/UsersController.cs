using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using serverSite.DTOs;
using serverSite.Interfaces;

namespace serverSite.Controllers
{
    [EnableCors]
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;              
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
        {
            return Ok(await _userRepository.GetMembersAsync());
        }

        [HttpGet("GetById/{Id}")]
        public async Task<ActionResult<MemberDTO>> GetUser(int Id)
        {
            return await _userRepository.GetMemberByIdAsync(Id);
        }

        [HttpGet("GetByName/{UserName}")]
        public async Task<ActionResult<MemberDTO>> GetUser(string UserName)
        {
            return await _userRepository.GetMemberByNameAsync(UserName);
        }
    }
}