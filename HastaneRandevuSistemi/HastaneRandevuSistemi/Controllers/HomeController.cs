using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HastaneRandevuSistemi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Uri baseAddress = new Uri("https://localhost:7078/api/Db/"); // API address
        private readonly HttpClient _client;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        public class veriler
        {
            public string Email { get; set; }
        }
        IEnumerable<veriler> students = null;
        public IActionResult Index()
        {
            List<veriler> reservationList = new List<veriler>();
            using (var httpClient = new HttpClient())
            {
                using (var response =  httpClient.GetAsync("https://localhost:44324/api/Reservation"))
                {
                    string apiResponse =  response.Content.ReadAsStringAsync();
                    reservationList = JsonContent.DeserializeObject<List<veriler>>(apiResponse);
                }
            }
            return View(reservationList);
            //var response = _client.GetAsync(_client.BaseAddress + "/Db/GetUsers").Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    var date = response.Content.ReadAsAsync<IList<veriler>>();
            //    CSharpMember member = JsonSerializer.Deserialize<CSharpMember>(date);
            //    Console.WriteLine($"Name: {member.Email}");
            //}
            //return View();
        }
        public IActionResult AdminPanel()
        {
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
