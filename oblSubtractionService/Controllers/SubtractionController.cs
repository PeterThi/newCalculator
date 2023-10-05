using Microsoft.AspNetCore.Mvc;

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
            Console.WriteLine("Subtraction serviced recieved request!");

            var client = new HttpClient();
            client.BaseAddress = new Uri("http://historyService/History");

            var response = client.PostAsync(client.BaseAddress + "?leftNumber=" + leftNumber + "&rightNumber=" + rightNumber + "&isAddition=" + false + "&result=" + (leftNumber - rightNumber), null);
            Console.WriteLine("subtracted " + leftNumber + " + " + rightNumber);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;

        }
    }
}