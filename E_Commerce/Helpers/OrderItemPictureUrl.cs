using AutoMapper;
using E_Commerce.Dtos;
using E_Commerece.Core.Models.Order_Aggreation;

namespace E_Commerce.Helpers
{
    public class OrderItemPictureUrl : IValueResolver<OrderItem,OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrl(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
                return null;

            var baseUrl = _configuration["BaseUrl"];
            return $"{baseUrl}{source.Product.PictureUrl}";
        }
    }

}
