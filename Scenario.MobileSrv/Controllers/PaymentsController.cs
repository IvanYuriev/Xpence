using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Scenario.MobileSrv.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> _logger;
        private readonly IPaymentsService _service;

        public PaymentsController(
            ILogger<PaymentsController> logger,
            IPaymentsService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<PaymentModel>> Get([FromQuery] int page = 0, [FromQuery] int size = 100)
        {
            _logger.LogInformation($"Payment starting with User {User?.Identity.Name}");
            return await _service.Get(User, page, size);
        }
    }
}
