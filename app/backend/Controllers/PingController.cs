using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : Controller
    {
        /// <summary>
        /// Healhcheck to make sure the service is up.
        /// </summary>
        /// <response code="200">The service is up and running.</response>
        [HttpGet]
        public string Get()
        {
            return "pong";
        }
    }
}
