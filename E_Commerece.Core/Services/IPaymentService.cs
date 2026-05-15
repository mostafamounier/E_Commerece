using E_Commerece.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Core.Services
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string BasketId);
        public Task UpdateOrderStastusSuccessOrFailedAsync(String intendId, bool flag);
    }
}
