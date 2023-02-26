using serverSite.Entities;

namespace serverSite.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
        string CreateToken(AppUser user, string secretKey);
    }
}