using Microsoft.AspNetCore.Mvc;
using Monitoring;
using MySql.Data.MySqlClient;
using OpenTelemetry;
using OpenTelemetry.Context.Propagation;
using Polly;
using Serilog;
//using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;

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
            using (var activity = MonitoringService.ActivitySource.StartActivity())
            {
                var ActivityContext = activity?.Context ?? Activity.Current?.Context ?? default;
            
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

                    var request = new HttpRequestMessage(HttpMethod.Post, client.BaseAddress + "?leftNumber=" + leftNumber + "&rightNumber=" + rightNumber + "&isAddition=" + true + "&result=" + (leftNumber + rightNumber));
                    var response = client.SendAsync(request);
                    
                    //Distributed tracing starts.
                    var propagationContext = new PropagationContext(ActivityContext, Baggage.Current);
                    var propagator = new TraceContextPropagator();
                    propagator.Inject(propagationContext, request, (r, Key, value) =>
                    {
                        r.Headers.Add(Key, value);
                    });


                    Console.WriteLine("Added " + leftNumber + " + " + rightNumber);
                    result = response.Result.Content.ReadAsStringAsync().Result;
                });
                return result;
            };

        }
    }
}