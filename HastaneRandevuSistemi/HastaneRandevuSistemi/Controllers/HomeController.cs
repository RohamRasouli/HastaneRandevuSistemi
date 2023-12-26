using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

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
        public IActionResult AdminPanel()
        {
            List<User> users = new List<User>();
            var response = _client.GetAsync(_client.BaseAddress + "Db/GetAllUsers").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<User>>(data);
            }
            ViewBag.data = users;
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
            _user.CreatedDate= DateTime.Now;
            _user.LastLoginDate = null;
            _user.UserTypeId= 2;
            try
            {
                string data = JsonConvert.SerializeObject(_user);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responce = _client.PostAsync(_client.BaseAddress + "Db/AddUser", content).Result;

                if (responce.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Kullanıcı kaydı başarılı";
                    return RedirectToAction("AdminPanel");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Kullanıcı kaydı başarısız";
                return View();
            }
            return View();
        }
        public IActionResult LogIn() 
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
