using DbAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetUsers()
        {
            //using (var _context = new DbHastaneContext())
            //{
            //    return _context.Users.ToList();
            //}

            var _context = new DbHastaneContext();
            if (_context != null)
            {
                return Ok(await _context.UserTypes.ToListAsync());
            }
            return BadRequest();
            return null;
        }
        [HttpGet]
        [Route("[action]/{id:int}")]
        public async Task<IActionResult> GetUsersById(int id)
        {
            //using (var _context = new DbHastaneContext())
            //{
            //    return _context.Users.ToList();
            //}                

            var _context = new DbHastaneContext();
            if (_context != null)
            {
                return Ok(await _context.UserTypes.Where(C => C.UserTypeId == id).ToListAsync());
            }
            return BadRequest();
            return null;
        }
        
    }
}
