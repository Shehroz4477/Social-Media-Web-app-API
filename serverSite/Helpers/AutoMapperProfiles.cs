using AutoMapper;
using serverSite.DTOs;
using serverSite.Entities;
using serverSite.Extensions;

namespace serverSite.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser,MemberDTO>()
                .ForMember
                (
                    destinationMember => destinationMember.PhotoUrl,
                    options => options.MapFrom(sourceMember => sourceMember.Photos.FirstOrDefault(photo => photo.IsMain).Url)
                )
                .ForMember
                (
                    destinationMember => destinationMember.age,
                    opttions => opttions.MapFrom(sourceMember => sourceMember.DateOfBirth.CalculateAge())
                );
            CreateMap<Photo,PhotoDTO>();
            CreateMap<MemberUpdateDTO, MemberDTO>();
        }
    }
}