using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebMVC.Helpers;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        private static string webUrlAPI = "https://localhost:44351";

        public async Task<IActionResult> Index()
        {
            UserToken token = null;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(webUrlAPI);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                LoginModel loginModel = new LoginModel();
                loginModel.Username = "hoaidq";
                loginModel.Password = "Admin@123";

                var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");

                var responseMessage = await client.PostAsync("api/Authenticate/login", content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = responseMessage.Content.ReadAsStringAsync().Result;
                    token = JsonConvert.DeserializeObject<UserToken>(resultMessage);

                    SessionHelper.SetObjectAsJson(HttpContext.Session, "UserToken", token);
                }
            }

            return Content(JsonConvert.SerializeObject(token));
        }

        public IActionResult Privacy()
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
