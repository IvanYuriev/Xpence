using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Scenario.AuthSrv.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentsController : ControllerBase
    {
        private HttpClient _client;

        public PaymentsController(IHttpClientFactory factory)
        {
            //TODO: replace named client with a proper type-safe one!
            _client = factory.CreateClient("mobile");
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