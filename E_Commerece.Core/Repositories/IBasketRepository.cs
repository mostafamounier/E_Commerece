using E_Commerece.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Core.Repositories
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasket(string id);
        Task<CustomerBasket> UpdateBasket(CustomerBasket id);
        Task<bool> DeleteBasket(string id);
    }
}
