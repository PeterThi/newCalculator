using Microsoft.AspNetCore.Mvc;

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
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://historyService/History");

            var request = new HttpRequestMessage(HttpMethod.Put, client.BaseAddress);
            var response = client.SendAsync(request);

            //Distributed tracing starts.
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