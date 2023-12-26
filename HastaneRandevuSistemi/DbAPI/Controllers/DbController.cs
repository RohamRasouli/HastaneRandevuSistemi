
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
        public async Task<IActionResult> UserLogin(string email,string password)
        {
            var _context = new DbHastaneContext();
            var results = from user in _context.Users
                          join user_type in _context.UserTypes on user.UserTypeId equals user_type.UserTypeId
                          where user.UserEmail.Equals(email) & user.Password.Equals(password)
                          select new
                          {
                              user.UserFirstName,
                              user.UserSecondName,
                              user.UserEmail,
                              user.Password,
                              user.LastLoginDate,
                              user.CreatedDate,
                              user_type.TypeName
                          };
            if (results == null || results.ToList().Count == 0)
            {
                return BadRequest("Kullanýcý bulunamadý");
            }
            return Ok(_context.Users.ToList());
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var _context = new DbHastaneContext();
            return Ok(_context.Users.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(Models.User _user)
        {
            if (_user == null)
                return BadRequest("Hata: Kullanýcý null!");
            else if(_user.UserEmail==null)
                return BadRequest("Hata: Mail adresi boþ olamaz!");
            else if (_user.Password == null)
                return BadRequest("Hata: Parola boþ olamaz!");
            else if (_user.UserEmail == null || _user.UserSecondName==null)
                return BadRequest("Hata: Ýsim-Soyisim boþ olamaz!");

            var _context = new DbHastaneContext();
            _context.Users.Add(_user);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }

    }
}
