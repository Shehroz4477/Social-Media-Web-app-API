using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serverSite.Data;
using serverSite.Entities;

namespace serverSite.Controllers
{
    [AllowAnonymous]
    public class BuggyController: BaseApiController
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;           
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "Secret text";
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest(){
            return BadRequest("This was not a good request");
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError(){
            var user = _context.Users.Find(-1);
            var userToReturn = user.ToString();
            return userToReturn;
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound(){
            var user = _context.Users.Find(-1);
            if(user == null) return NotFound();
            return user;
        }
    }
}