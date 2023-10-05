using Microsoft.AspNetCore.Mvc;
using MVCGui.Models;
using System.Diagnostics;
using Polly;

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
            //fault isolation
            var retryPolicy = Policy.Handle<HttpRequestException>()
                .Retry(3, (exception, retryCount) =>
                {
                    Console.WriteLine("Retrying because MVCGui ran into" + exception.GetType().Name);
                });

            var fallbackPolicy = Policy.Handle<HttpRequestException>()
                .Fallback(() =>
                 {
                     //if services cannot be reached, we complete the action ourselves.
                     ViewBag.calculationList = leftNumber + rightNumber;
                    
                 });
            var policy = retryPolicy.Wrap(fallbackPolicy);

            policy.Execute(() =>
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://additionService/Addition");

                var response = client.PostAsync(client.BaseAddress + "?leftNumber=" + leftNumber + "&rightNumber=" + rightNumber, null);
                Console.WriteLine("Added " + leftNumber + " + " + rightNumber);
                string result = response.Result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result + "RESULTED!!");




             if (!string.IsNullOrEmpty(result))
                {
                    List<string> stringList = result.Split(',').ToList();
                    for (int i = 0; i < stringList.Count; i++)
                    {
                        stringList[i] = stringList[i].Trim().Trim('[', ']').Replace("\"", "");
                    }
                    ViewBag.calculationList = stringList;
                }
            });
            
            if (ViewBag.calculationList == null)
            {
                ViewBag.CalculationList = new List<String>{"something went wrong"};
            }
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

            //fault isolation
            if (string.IsNullOrEmpty(result))
            {
                ViewBag.calculationList = leftNumber - rightNumber;
                return View("Index");
            }

            List<string> stringList = result.Split(',').ToList();
            for (int i = 0; i < stringList.Count; i++)
            {
                stringList[i] = stringList[i].Trim().Trim('[', ']').Replace("\"", "");
            }
            ViewBag.calculationList = stringList;
            return View("Index");
        }
    }
}