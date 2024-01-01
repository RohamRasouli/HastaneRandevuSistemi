using HastaneRandevuSistemi.Models;
using HastaneRandevuSistemi.Models.AddClass;
using HastaneRandevuSistemi.Models.ReturnClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace HastaneRandevuSistemi.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        Uri baseAddress = new Uri("https://localhost:7078/api/"); // API address
        private readonly HttpClient _client;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
      
        [HttpGet]
        [Authorize(Roles = "Kullanıcı, Doktor")]
        public IActionResult UserHome()
        {
            List<AllUsersInfo> doctors = new List<AllUsersInfo>();
            var response = _client.GetAsync(_client.BaseAddress + "Db/GetAllDoctors").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                doctors = JsonConvert.DeserializeObject<List<AllUsersInfo>>(data);
            }
            ViewBag.doctors = doctors;

            List<AllAppointments> appointments = new List<AllAppointments>();
            var response_2 = _client.GetAsync(_client.BaseAddress + "Db/GetUserAppointment/" + User.FindFirstValue(ClaimTypes.Sid).ToString()).Result;
            if (response_2.IsSuccessStatusCode)
            {
                var data = response_2.Content.ReadAsStringAsync().Result;
                appointments = JsonConvert.DeserializeObject<List<AllAppointments>>(data);
            }
            ViewBag.appointments = appointments;

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
        [HttpPost]
        public IActionResult MakeAppointment(int main_branch, int policlinic, int doctor, string date, string time)
        {
            Appointment _app = new Appointment();
            _app.UserId= int.Parse(User.FindFirstValue(ClaimTypes.Sid));
            _app.StatuId = 1;
            _app.DoctorId = doctor;
            _app.PoliclinicId = policlinic;
            _app.MainScienceBranchId = main_branch;
            _app.AppointmentDate = Convert.ToDateTime(date + " " + time + ":00.000");
            _app.CreatetDate = DateTime.Now;
                  

            try
            {
                string data = JsonConvert.SerializeObject(_app);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responce = _client.PostAsync(_client.BaseAddress + "Db/AddAppointment", content).Result;

                if (responce.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Randevu kaydı başarılı";
                    return RedirectToAction("UserHome");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Randevu kaydı başarısız";
                return View();
            }
            return View();
        }

        [HttpGet]
        [Route("[controller]/[action]/{a_id}")]
        public IActionResult DeleteAppointment(int a_id)
        {
            try
            {
                HttpResponseMessage responce = _client.DeleteAsync(_client.BaseAddress + "Db/DeleteAppointment/" + a_id.ToString()).Result;

                if (responce.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Silme başarılı";
                    return Redirect(Request.Headers["Referer"].ToString());
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Silme başarısız!!";
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
