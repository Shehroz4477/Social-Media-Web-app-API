using AutoMapper;
using serverSite.DTOs;
using serverSite.Entities;

namespace serverSite.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser,MemberDTO>().ForMember
            (
                destinationMember => destinationMember.PhotoUrl,
                options => options.MapFrom
                (   
                    source => source.Photos.FirstOrDefault(photo => photo.IsMain).Url
                )
            );
            CreateMap<Photo,PhotoDTO>();
        }
    }
}