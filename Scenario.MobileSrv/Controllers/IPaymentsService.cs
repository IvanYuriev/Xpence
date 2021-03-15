using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Scenario.MobileSrv.Controllers
{
    public interface IPaymentsService
    {
        Task<List<PaymentModel>> Get(ClaimsPrincipal user, int page, int size);
    }
}
