
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using HastaneRandevuSistemi.Models;
using Newtonsoft.Json.Linq;
using HastaneRandevuSistemi.Models.ReturnClass;
using HastaneRandevuSistemi.Models.AddClass;

namespace HastaneRandevuSistemi.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Uri baseAddress = new Uri("https://localhost:7078/api/"); // API address
        private readonly HttpClient _client;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
    
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            // GetAllUsers
            List<AllUsersInfo> users = new List<AllUsersInfo>();
            var response = _client.GetAsync(_client.BaseAddress + "Db/GetAllUsers").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<AllUsersInfo>>(data);
            }
            ViewBag.users = users;

            List<AllUsersInfo> doctors = new List<AllUsersInfo>();
            var response_2 = _client.GetAsync(_client.BaseAddress + "Db/GetAllDoctors").Result;
            if (response_2.IsSuccessStatusCode)
            {
                var data = response_2.Content.ReadAsStringAsync().Result;
                doctors = JsonConvert.DeserializeObject<List<AllUsersInfo>>(data);
            }
            ViewBag.doctors = doctors;

            List<AllWorkTimes> _wt = new List<AllWorkTimes>();
            var response_3 = _client.GetAsync(_client.BaseAddress + "Db/GetAllWorkTimes").Result;
            
            if (response_3.IsSuccessStatusCode)
            {
                var data = response_3.Content.ReadAsStringAsync().Result;
                _wt = JsonConvert.DeserializeObject<List<AllWorkTimes>>(data);
            }
            ViewBag.workTimes = _wt;

            List<AllScienceBranchs> branchs = new List<AllScienceBranchs>();
            var response_4 = _client.GetAsync(_client.BaseAddress + "Db/GetAllBranchs").Result;
            if (response_4.IsSuccessStatusCode)
            {
                var data = response_4.Content.ReadAsStringAsync().Result;
                branchs = JsonConvert.DeserializeObject<List<AllScienceBranchs>>(data);
            }
            ViewBag.branchs = branchs;

            List<AllPoliclinics> policlinics = new List<AllPoliclinics>();
            var response_5 = _client.GetAsync(_client.BaseAddress + "Db/GetAllPoliclinics").Result;
            if (response_5.IsSuccessStatusCode)
            {
                var data = response_5.Content.ReadAsStringAsync().Result;
                policlinics = JsonConvert.DeserializeObject<List<AllPoliclinics>>(data);
            }
            ViewBag.policlinics = policlinics;

            return View();
        }
        [HttpGet]
        public IActionResult SignIn()
        {

            return View();
        }
        [HttpPost]
        public IActionResult SignIn(string fname,string sname,string email,string password)
        {
            User _user = new User();
            _user.UserFirstName = fname;
            _user.UserSecondName = sname;
            _user.UserEmail = email;
            _user.Password = password;
            _user.CreatedDate = DateTime.Now;
            _user.LastLoginDate = null;
            _user.UserTypeId = 2; // default 2 atanır
            try
            {
                string data = JsonConvert.SerializeObject(_user);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responce = _client.PostAsync(_client.BaseAddress + "Db/AddUser", content).Result;

                if (responce.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Kullanıcı kaydı başarılı";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Kullanıcı kaydı başarısız!!";
                return View();
            }
            return View();
        }

       
        [HttpGet]
        public IActionResult LogIn()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(string email, string password)
        {
            AllUsersInfo _user = new AllUsersInfo();
            List<AllUsersInfo> users = new List<AllUsersInfo>();
            var response = _client.GetAsync(_client.BaseAddress + "Db/UserLogin/" + email.ToString() + "," + password.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<AllUsersInfo>>(data);
                TempData["email"] = users[0].UserEmail;

                // LOGİN
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,users[0].UserEmail.ToString()),
                    new Claim(ClaimTypes.Sid, users[0].UserId.ToString()),
                    new Claim(ClaimTypes.Role,users[0].TypeName)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = true
                };


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                   new ClaimsPrincipal(claimsIdentity), properties);
                //var yetki = User.FindAll(ClaimTypes.Role).ToList();

                var userIdentity = (ClaimsIdentity)User.Identity;
                var claims_ = userIdentity.Claims;
                var roleClaimType = userIdentity.RoleClaimType;
                var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
               
                if (roles.First().Value.Equals("Admin"))
                    return RedirectToAction("AdminPanel", "Home");
                else
                    return RedirectToAction("UserHome", "User");
            }
            TempData["message"] = "Kullanıcı bulunamadı. Lütfen bilgilerinizi kontrol ediniz";
            return View();
        }
        [HttpGet]
        public  IActionResult LogOut()
        {
             HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LogIn", "Home");

        }

        [HttpPost]
        public IActionResult AddDoctorWorkTime(int doctor, int policlinic,int main_branch,string start_date, string start_time,string end_date,string end_time)
        {
            DoctorWorkTime _dwt = new DoctorWorkTime();
            _dwt.DoctorId = doctor;
            _dwt.PoliclinicId = policlinic;
            _dwt.MainBranchId = main_branch;
            _dwt.StartDate = Convert.ToDateTime(start_date +" " +start_time+":00.000");
            _dwt.EndDate = Convert.ToDateTime(end_date +" " + end_time+":00.000");
            _dwt.CreatedDate = DateTime.Now;
            try
            {
                string data = JsonConvert.SerializeObject(_dwt);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responce = _client.PostAsync(_client.BaseAddress + "Db/AddDoctorWorkTime", content).Result;

                if (responce.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Çalışma saati kaydı başarılı";
                    return RedirectToAction("AdminPanel");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Çalışma saati kaydı başarısız";
                return View();
            }
            return View();
        }

        public IActionResult GetAllWorkTimes() 
        {
            List<AllWorkTimes> _wt = new List<AllWorkTimes>();
            var response = _client.GetAsync(_client.BaseAddress + "Db/GetAllWorkTimes").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                _wt = JsonConvert.DeserializeObject<List<AllWorkTimes>>(data);
            }
            ViewBag.data = _wt;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return null;
        }
    }
}
