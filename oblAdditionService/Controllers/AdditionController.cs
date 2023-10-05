using Microsoft.AspNetCore.Mvc;
using Monitoring;
using MySql.Data.MySqlClient;
using Polly;
using Serilog;
//using MySql.Data.MySqlClient;
using System.Data;
namespace oblAdditionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdditionController : ControllerBase
    {

        public AdditionController()
        {

        }

        [HttpPost]
        public string Add(int leftNumber, int rightNumber)
        {
            using (var activity = MonitoringService.ActivitySource.StartActivity());
            
                MonitoringService.Log.Debug("Started adding in addService");
                string result = leftNumber + " + " + rightNumber + " = " + (leftNumber + rightNumber);
                var fallbackPolicy = Policy.Handle<Exception>()
                    .Fallback(() =>
                    {
                        //if database cannot be reached, just return the numbers added up.
                        MonitoringService.Log.Error("Exception caught, returning singular addition");
                    });

                fallbackPolicy.Execute(() =>
                {

                    var client = new HttpClient();
                    client.BaseAddress = new Uri("http://historyService/History");

                    var response = client.PostAsync(client.BaseAddress + "?leftNumber=" + leftNumber + "&rightNumber=" + rightNumber + "&isAddition=" + true + "&result=" + (leftNumber + rightNumber), null);
                    Console.WriteLine("Added " + leftNumber + " + " + rightNumber);
                    result = response.Result.Content.ReadAsStringAsync().Result;
                });
                return result;
            
            
        }
    }
}