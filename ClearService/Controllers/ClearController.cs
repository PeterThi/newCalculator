using Microsoft.AspNetCore.Mvc;
using Monitoring;
using OpenTelemetry;
using OpenTelemetry.Context.Propagation;
using System.Diagnostics;

namespace ClearService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClearController : ControllerBase
    {

        public ClearController()
        {
            
        }

        [HttpGet]
        public String Get()
        {
            using (var activity = MonitoringService.ActivitySource.StartActivity())
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://historyService/History");

                var request = new HttpRequestMessage(HttpMethod.Put, client.BaseAddress);
                var response = client.SendAsync(request);

                //Distributed tracing starts.
                var ActivityContext = activity?.Context ?? Activity.Current?.Context ?? default;
                var propagationContext = new PropagationContext(ActivityContext, Baggage.Current);
                var propagator = new TraceContextPropagator();
                propagator.Inject(propagationContext, request, (r, Key, value) =>
                {
                    r.Headers.Add(Key, value);
                });

                var result = response.Result.Content.ReadAsStringAsync().Result;
                return "Calculator was cleared (ish)" + result;
            }




        }

    }
}