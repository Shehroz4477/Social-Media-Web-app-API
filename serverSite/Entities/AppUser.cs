using System.ComponentModel.DataAnnotations;

namespace serverSite.Entities
{
    public class AppUser
    {
        // specify the primary key option
        [Key] 
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}