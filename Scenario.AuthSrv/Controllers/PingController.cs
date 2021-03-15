using Microsoft.AspNetCore.Mvc;

namespace Scenario.AuthSrv.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Pong";
        }
    }
}