using Microsoft.AspNetCore.Mvc;
using MVCGui.Models;
using System.Diagnostics;

namespace MVCGui.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(int leftNumber, int rightNumber)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://additionService/Addition");

            var response = client.PostAsync(client.BaseAddress + "?leftNumber=" + leftNumber + "&rightNumber=" + rightNumber, null);
            Console.WriteLine("Added " + leftNumber + " + " + rightNumber);
            String result = response.Result.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result + "RESULTED!!");
            return View("Index");
        }
    }
}