using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using serverSite.Data;
using serverSite.DTOs;
using serverSite.Interfaces;

namespace serverSite.Controllers
{
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
            return Ok(_mapper.Map<IEnumerable<MemberDTO>>(await _userRepository.GetUsersAsync()));
        }

        [HttpGet("GetById/{Id}")]
        public async Task<ActionResult<MemberDTO>> GetUser(int Id)
        {
            return _mapper.Map<MemberDTO>(await _userRepository.GetUserByIdAsync(Id));
        }

        [HttpGet("GetByName/{UserName}")]
        public async Task<ActionResult<MemberDTO>> GetUser(string UserName)
        {
            return _mapper.Map<MemberDTO>(await _userRepository.GetUserByNameAsync(UserName));
        }
    }
}