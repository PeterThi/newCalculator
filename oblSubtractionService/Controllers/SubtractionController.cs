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
        public int PostSubtract(int leftNumber, int rightNumber)
        {
            Console.WriteLine("Subtraction serviced recieved request!");
            return leftNumber - rightNumber;

        }
    }
}