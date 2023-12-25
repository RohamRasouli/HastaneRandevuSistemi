using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
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
        public class User
        {
            public int UserId { get; set; }
            public string? UserFirstName { get; set; }
            public string? UserSecondName { get; set; }
            public string? UserEmail { get; set; }
            public int? UserTypeId { get; set; }
            public DateTime? CreatedDate { get; set; }
            public DateTime? LastLoginDate { get; set; }
            public string? Password { get; set; }
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AdminPanel()
        {
            List<User> users = new List<User>();
            var response = _client.GetAsync(_client.BaseAddress + "Db/GetUsers").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = "[{\"DeptId\": 101,\"DepartmentName\":\"IT\" }, {\"DeptId\": 102,\"DepartmentName\":\"Accounts\" }]";
                data = response.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<User>>(data);
            }
            ViewBag.data = users;
            return View();
        }
        public IActionResult SignIn()
        {
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
