using System.Collections;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using serverSite.DTOs;
using serverSite.Entities;
using serverSite.Extensions;
using serverSite.Interfaces;

namespace serverSite.Controllers
{
    [EnableCors]
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoServices _photoServices;
        public UsersController(IUserRepository userRepository, IMapper mapper,IPhotoServices photoServices)
        {
            _photoServices = photoServices;
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

        [HttpPut("update")]
        public async Task<ActionResult> UpdateMember(MemberUpdateDTO memeberUpdate)
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if(userName != null)
            {
               if(await _userRepository.UpdateMemberAsync(memeberUpdate,userName)) return NoContent();
                BadRequest("Failed to update user");
            }
            return NotFound();
        }

        [HttpPost("addPhoto")]
        public async Task<ActionResult<PhotoDTO>> AddPhoto(IFormFile file)
        {
            var user = await _userRepository.GetUserByNameAsync(User.GetUserName());
            if(user  == null) return NotFound();
            var result = await _photoServices.AddPhoto(file);
            if(result.Error != null) return BadRequest(result.Error.Message);
            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                IsMain = false
            };
            if(user.Photos.Count == 0) photo.IsMain = true;
            user.Photos.Add(photo);
            if(await _userRepository.SaveAllAsync())
            {
                return CreatedAtAction
                (
                    nameof(GetUser), 
                    new {userName = user.UserName},
                    _mapper.Map<PhotoDTO>(photo)
                );
            }
            return BadRequest("Faild to upload photo");
        }
    }
}