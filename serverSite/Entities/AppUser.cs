using System.ComponentModel.DataAnnotations;

namespace serverSite.Entities
{
    public class AppUser
    {
        [Key] // specify the primary key option
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}