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
            List<String> stringList = new List<string>();
            stringList.Add("Future calculations shown here");
            ViewBag.calculationList = stringList;
            return View();
        }

        [HttpPost]
        public IActionResult Add(int leftNumber, int rightNumber)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://additionService/Addition");

            var response = client.PostAsync(client.BaseAddress + "?leftNumber=" + leftNumber + "&rightNumber=" + rightNumber, null);
            Console.WriteLine("Added " + leftNumber + " + " + rightNumber);
            string result = response.Result.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result + "RESULTED!!");


            List<string> stringList = result.Split(',').ToList();
            for (int i= 0; i < stringList.Count; i++)
            {
                stringList[i] = stringList[i].Trim().Trim('[', ']').Replace("\"", "");
            }
            ViewBag.calculationList = stringList;
            return View("Index");
        }

        [HttpPost]
        public IActionResult Subtract(int leftNumber, int rightNumber)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://SubtractionService/Subtraction");

            var response = client.PostAsync(client.BaseAddress + "?leftNumber=" + leftNumber + "&rightNumber=" + rightNumber, null);
            Console.WriteLine("Subtracted " + leftNumber + " - " + rightNumber);
            String result = response.Result.Content.ReadAsStringAsync().Result;
            
            Console.WriteLine(result + "subtraction RESULTED!!");
            return View("Index");
        }
    }
}