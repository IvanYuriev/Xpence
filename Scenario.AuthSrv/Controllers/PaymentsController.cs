using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Scenario.AuthSrv.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1")]
    public class PaymentsController : ControllerBase
    {
        private HttpClient _client;

        public PaymentsController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("mobile");;
        }   

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //Append JWT token or better pass it through!
            //_client.Headers.Add("");
            var msg = await _client.GetAsync("payments");
            return Ok(await msg.Content.ReadAsStringAsync());
        }
    }
}