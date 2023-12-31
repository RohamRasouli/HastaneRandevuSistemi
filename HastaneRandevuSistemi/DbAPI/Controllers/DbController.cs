

using DbAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbAPI.Controllers
{
    [ApiController]
    public class DbController : ControllerBase
    {
        //private readonly DbHastaneContext _context;

        //public DbController(DbHastaneContext context)
        //{
        //    //_context = context;
        //}

        [HttpGet]
        [Route("api/[controller]/[action]/{email},{password}")]
        public async Task<IActionResult> UserLogin(string email,string password)
        {
            var _context = new DbHastaneContext();
            var results = from user in _context.Users
                          join user_type in _context.UserTypes on user.UserTypeId equals user_type.UserTypeId
                          where user.UserEmail.Equals(email) & user.Password.Equals(password)
                          select new
                          {
                              user.UserId,
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
            return Ok(results.ToList());
        }
        
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> GetAllUsers()
        {
            var _context = new DbHastaneContext();
            var results = from user in _context.Users
                          join user_type in _context.UserTypes on user.UserTypeId equals user_type.UserTypeId
                          //where user.UserTypeId.Equals(2) // doktor tipi
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
                return BadRequest("Veri bulunamadý");
            }

           
            return Ok(results.ToList());
        }
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> GetAllPoliclinics()
        {
            var _context = new DbHastaneContext();
            var results = from policlinic in _context.Policlinics
                          select new
                          {
                              policlinic.PoliclinicId,
                              policlinic.PoliclinicName
                          };
            if (results == null || results.ToList().Count == 0)
            {
                return BadRequest("Veri bulunamadý");
            }
            return Ok(results.ToList());
        }
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> GetAllBranchs()
        {
            var _context = new DbHastaneContext();
            var results = from bracnhs in _context.MainScienceBranches
                          select new
                          {
                              bracnhs.MainScienceBranchýd,
                              bracnhs.ScienceName
                          };
            if (results == null || results.ToList().Count == 0)
            {
                return BadRequest("Veri bulunamadý");
            }
            return Ok(results.ToList());
        }
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> AddUser(Models.User _user)
        {
            if (_user == null)
                return BadRequest("Hata: Kullanýcý null!");
            else if (_user.UserEmail == null)
                return BadRequest("Hata: Mail adresi boþ olamaz!");
            else if (_user.Password == null)
                return BadRequest("Hata: Parola boþ olamaz!");
            else if (_user.UserEmail == null || _user.UserSecondName == null)
                return BadRequest("Hata: Ýsim-Soyisim boþ olamaz!");

            var _context = new DbHastaneContext();
            _context.Users.Add(_user);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }
        [Route("api/[controller]/[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var _context = new DbHastaneContext();
            var results = from user in _context.Users
                          join user_type in _context.UserTypes on user.UserTypeId equals user_type.UserTypeId
                          where user.UserTypeId.Equals(3) // doktor tipi
                          select new
                          {
                              user.UserId,
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
                return BadRequest("Veri bulunamadý");
            }
            return Ok(results.ToList());
        }
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> AddDoctorWorkTime(Models.DoctorWorkTime _workTime)
        {
            if (_workTime == null)
                return BadRequest("Hata: Nesne null!");
            else if (_workTime.DoctorId == null)
                return BadRequest("Hata: Doktor boþ olamaz!");
            else if (_workTime.PoliclinicId == null)
                return BadRequest("Hata: Poliklinik boþ olamaz!");
            else if (_workTime.StartDate == null || _workTime.EndDate == null)
                return BadRequest("Hata: Baþlangýç veya Bitiþ tarihi boþ olamaz!");

            var _context = new DbHastaneContext();
            _context.DoctorWorkTimes.Add(_workTime);
            await _context.SaveChangesAsync();
            return Ok(await _context.DoctorWorkTimes.ToListAsync());
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> GetAllWorkTimes()
        {
            //var _context = new DbHastaneContext();
            //return Ok(_context.DoctorWorkTimes.ToList());

            var _context = new DbHastaneContext();
            var results = from DoctorWorkTime in _context.DoctorWorkTimes
                          join user in _context.Users on DoctorWorkTime.DoctorId equals user.UserId
                          join mainBranch in _context.MainScienceBranches on DoctorWorkTime.MainBranchId equals mainBranch.MainScienceBranchýd
                          join policlinic in _context.Policlinics on DoctorWorkTime.PoliclinicId equals policlinic.PoliclinicId
                          select new
                          {
                              user.UserFirstName,
                              user.UserSecondName,
                              policlinic.PoliclinicName,
                              mainBranch.ScienceName,
                              DoctorWorkTime.StartDate,
                              DoctorWorkTime.EndDate,
                              user.CreatedDate,
                          };
            if (results == null || results.ToList().Count == 0)
            {
                return BadRequest("Veri bulunamadý");
            }
            return Ok(results.ToList());
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> AddAppointment(Models.Appointment _appointment)
        {
            if (_appointment == null)
                return BadRequest("Hata: Nesne null!");
            else if (_appointment.DoctorId == null)
                return BadRequest("Hata: Doktor boþ olamaz!");
            else if (_appointment.PoliclinicId == null)
                return BadRequest("Hata: Poliklinik boþ olamaz!");
            else if (_appointment.AppointmentDate == null)
                return BadRequest("Hata: Randevu tarihi boþ olamaz!");

            var _context = new DbHastaneContext();
            _context.Appointments.Add(_appointment);
            await _context.SaveChangesAsync();
            return Ok(await _context.DoctorWorkTimes.ToListAsync());
        }
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> GetAllAppointment()
        {
            var _context = new DbHastaneContext();
            var results = from appointment in _context.Appointments
                          join user in _context.Users on appointment.DoctorId equals user.UserId
                          join mainBranch in _context.MainScienceBranches on appointment.MainScienceBranchId equals mainBranch.MainScienceBranchýd
                          join policlinic in _context.Policlinics on appointment.PoliclinicId equals policlinic.PoliclinicId
                          select new
                          {
                              user.UserFirstName,
                              user.UserSecondName,
                              policlinic.PoliclinicName,
                              mainBranch.ScienceName,
                              appointment.AppointmentDate,
                              appointment.CreatetDate,
                          };
            if (results == null || results.ToList().Count == 0)
            {
                return BadRequest("Veri bulunamadý");
            }
            return Ok(results.ToList());
        }
        [HttpGet]
        [Route("api/[controller]/[action]/{user_id}")]
        public async Task<IActionResult> GetUserAppointment(int user_id)
        {
            var _context = new DbHastaneContext();
            var results = from appointment in _context.Appointments
                          join user in _context.Users on appointment.UserId equals user.UserId
                          join doctor in _context.Users on appointment.DoctorId equals doctor.UserId
                          join mainBranch in _context.MainScienceBranches on appointment.MainScienceBranchId equals mainBranch.MainScienceBranchýd
                          join policlinic in _context.Policlinics on appointment.PoliclinicId equals policlinic.PoliclinicId
                          where appointment.UserId.Equals(user_id)
                          select new
                          {
                              user.UserFirstName,
                              user.UserSecondName,
                              doctorFirstName = doctor.UserFirstName,
                              doctorSecondName = doctor.UserSecondName,
                              policlinic.PoliclinicName,
                              mainBranch.ScienceName,
                              appointment.AppointmentDate,
                              appointment.CreatetDate,
                          };
            if (results == null || results.ToList().Count == 0)
            {
                return BadRequest("Kullanýcý bulunamadý");
            }
            return Ok(results.ToList());
        }
    }
}
