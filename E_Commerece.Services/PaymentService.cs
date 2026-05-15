using E_Commerece.Core;
using E_Commerece.Core.Models;
using E_Commerece.Core.Models.Order_Aggreation;
using E_Commerece.Core.Repositories;
using E_Commerece.Core.Services;
using E_Commerece.Core.Specifications;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Terminal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = E_Commerece.Core.Models.Product;

namespace E_Commerece.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration configuration;
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;

        public PaymentService(IConfiguration configuration,IBasketRepository basketRepository,IUnitOfWork unitOfWork)
        {
            this.configuration = configuration;
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
        }


        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string BasketId)
        {
            StripeConfiguration.ApiKey = configuration["stripsetting:SecretKey"];
            var basket =await basketRepository.GetBasket(BasketId);
            if (basket == null) return null;
            var shippingprice = 0m;
            if (basket.DeliveryMethodId.HasValue) {
             var deliverymethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
             shippingprice = deliverymethod.Cost;
             basket.ShippingCost = shippingprice;
            }
            if (basket?.Items?.Count > 0)
            {

                foreach (var item in basket.Items)
                {

                    var product = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    if (product.Price != item.Price)
                        item.Price = product.Price;
                }
            }
                PaymentIntent paymentIntent;
                    var service = new PaymentIntentService();
                    if (string.IsNullOrEmpty(basket.PaymentIntentId)) {

                        var options = new PaymentIntentCreateOptions()
                        {
                            Amount = (long)basket.Items.Sum(item => item.Price * item.qunatity * 100) + (long)shippingprice * 100,
                            Currency = "usd",
                            PaymentMethodTypes = new List<string>() { "card" }
                        };
                        paymentIntent = await service.CreateAsync(options);
                        basket.PaymentIntentId = paymentIntent.Id;
                        basket.ClinetSecret=paymentIntent.ClientSecret;
                    }
                    else
                    {
                        var options = new PaymentIntentUpdateOptions()
                        {
                            Amount = (long)basket.Items.Sum(item => item.Price * item.qunatity * 100) + (long)shippingprice * 100
                        };
                        await service.UpdateAsync(basket.PaymentIntentId,options);
                    }
                
                    await basketRepository.UpdateBasket(basket);

                    return basket;
                

            }
        public async Task UpdateOrderStastusSuccessOrFailedAsync(String intendId,bool flag)
        {
            OrderStatusSpecification spec = new OrderStatusSpecification(intendId);
            var order=await unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            if(order is not null)
            {
                if(flag)
                    order.status=OrderStatus.PaymentRecievrd;
                else
                    order.status=OrderStatus.PaymentFailed;
            }

        }


        }
    }

