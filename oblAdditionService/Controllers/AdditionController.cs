using Microsoft.AspNetCore.Mvc;

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
        public int Add(int leftNumber, int rightNumber)
        {
            Console.WriteLine("Got a request for :" + leftNumber);
            return 13;
        }
    }
}