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
            return "Calculator was cleared (ish)";
        }

    }
}