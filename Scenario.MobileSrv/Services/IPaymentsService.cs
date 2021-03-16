using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Scenario.MobileSrv.Models;

namespace Scenario.MobileSrv.Services
{
    public interface IPaymentsService
    {
        Task<List<PaymentModel>> Get(ClaimsPrincipal user, int page, int size);
    }
}
