using E_Commerece.Core;
using E_Commerece.Core.Models;
using E_Commerece.Core.Models.Order_Aggreation;
using E_Commerece.Core.Repositories;
using E_Commerece.Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order = E_Commerece.Core.Models.Order_Aggreation.Order;
namespace E_Commerece.Repository.Repositoriees
{
    public class OrderService : IOrderService
    {



        private readonly IBasketRepository basketRepository;

        private readonly IUnitOfWork unitOfWork;


        public OrderService()
        {

        }

        public OrderService(IBasketRepository basketRepository , IUnitOfWork unitOfWork)
        {
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;

        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, string BasketId, int deliveryMethodId, Address shippingAddress)
        {
            var basket = await basketRepository.GetBasket(BasketId);
            var items = basket.Items;
            var productitems = new List<OrderItem>();
            if (basket.Items != null)
            {
                foreach (var item in items)
                {
                    var product =await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    var productorderitem = new ProductOrderItem(item.Id, item.ProductName, item.ProductUrl);
                    var productorder = new OrderItem(productorderitem, item.qunatity,product.Price);

                    productitems.Add(productorder);

                    var subTotal =items.Sum(i=>i.qunatity*i.Price);
                    var deliveryMethod=await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
                    var Order =new Order(buyerEmail,deliveryMethod,shippingAddress,productitems,subTotal);
                    await unitOfWork.Repository<Order>().AddAsync(Order);
                    await  unitOfWork.Compelete();
                    return Order;
                }
            }
            return null;
        }

        public async Task<IEnumerable<DeliveryMethod>> GetAllDeliveryMethods()
        {

            return await unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
        }

        public async Task<Order> GetOrderByIDAsync(int OrderId, string buyerEmail)
        {

            var orderspec=new OrderSpecification(OrderId,buyerEmail);
            var order= await unitOfWork.Repository<Order>().GetByIdWithSpecAsync(orderspec);

            return order;

            
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(string Email)
        {
            var orderspec = new OrderSpecification(Email);
            var orders = await unitOfWork.Repository<Order>().GetAllWithSpecAsync(orderspec);


            return orders;
        }
    }
}
