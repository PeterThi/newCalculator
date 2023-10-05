using Microsoft.AspNetCore.Mvc;
using Polly;

namespace oblSubtractionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubtractionController : ControllerBase
    {
       

        public SubtractionController()
        {

        }

        [HttpPost]
        public string Subtract(int leftNumber, int rightNumber)
        {
            string result = leftNumber + " - " + rightNumber + " = " + (leftNumber - rightNumber);
            Console.WriteLine("Subtraction serviced recieved request!");

            var fallbackPolicy = Policy.Handle<Exception>()
                .Fallback(() =>
                {
                    //if database cannot be reached, just return the numbers added up.
                    Console.WriteLine("Fell back, returning singular subtraction");

                });
            fallbackPolicy.Execute(() =>
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://historyService/History");

                var response = client.PostAsync(client.BaseAddress + "?leftNumber=" + leftNumber + "&rightNumber=" + rightNumber + "&isAddition=" + false + "&result=" + (leftNumber - rightNumber), null);
                Console.WriteLine("subtracted " + leftNumber + " - " + rightNumber);
                result = response.Result.Content.ReadAsStringAsync().Result;
            });
            

            return result;

        }

       
    }
}