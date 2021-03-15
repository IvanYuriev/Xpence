using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Scenario.MobileSrv.Controllers
{
    public class PaymentsService : IPaymentsService
    {
        public Task<List<PaymentModel>> Get(ClaimsPrincipal user, int page, int size)
        {
            var result = new List<PaymentModel>();
            result.Add(new PaymentModel() { Amount = 100, From = "Mary", To = "Bob" });
            result.Add(new PaymentModel() { Amount = 147.5m, From = "Ann", To = "Mary" });
            return Task.FromResult(result);
        }
    }
}