using AutoMapper;
using serverSite.DTOs;
using serverSite.Entities;

namespace serverSite.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser,MemberDTO>();
            CreateMap<Photo,PhotoDTO>();
        }
    }
}