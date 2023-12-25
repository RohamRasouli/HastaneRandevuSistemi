using DbAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DbController : ControllerBase
    {
        //private DbHastaneContext _context;

        //public DbController(DbHastaneContext context)
        //{
        //    _context = context;
        //}

        [HttpGet]
        public async Task<IActionResult> GetUsers(/*string email,string password*/)
        {
            //            var _context = new DbHastaneContext();
            //            var results = from user in _context.Users
            //                          join user_type in _context.UserTypes on user.UserTypeId equals user_type.UserTypeId
            //                          where user.UserEmail.Equals(email) & user.Password.Equals(password)
            //                          select new
            //                          {
            //                              user.UserFirstName,
            //                              user.UserSecondName,
            //                              user.UserEmail,
            //                              user_type.UserTypeName
            //                          };
            //;           if (results == null || results.ToList().Count == 0)
            //            {
            //                return BadRequest("Kullanýcý bulunamadý");
            //            }
            var _context = new DbHastaneContext();
            return Ok(_context.Users.Select(x => x.UserEmail));
        }
        //[HttpGet]
        //[Route("[action]/{id:int}")]
        //public async Task<IActionResult> GetUsersById(int id)
        //{
        //    //using (var _context = new DbHastaneContext())
        //    //{
        //    //    return _context.Users.ToList();
        //    //}                

        //      var _context = new DbHastaneContext();
        //      return Ok(await _context.Users.Where(C => C.UserId == id).ToListAsync());
            
           
        //}

        [HttpPost]
        public async Task<IActionResult> AddUser(Models.User _user)
        {
            if (_user == null)
                return BadRequest();

            var _context = new DbHastaneContext();
            _context.Users.Add(_user);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }

    }
}
