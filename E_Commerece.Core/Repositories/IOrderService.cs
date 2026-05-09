using E_Commerece.Core.Models.Order_Aggreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Core.Repositories
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail,string BasketId,int deliveryMethodId, Address shippingAddress);
        Task<IEnumerable<Order>> GetUserOrdersAsync(string Email);
        Task<Order> GetOrderByIDAsync(int OrderId, string buyerEmail);
        Task<IEnumerable<DeliveryMethod>> GetAllDeliveryMethods();
    }
}
