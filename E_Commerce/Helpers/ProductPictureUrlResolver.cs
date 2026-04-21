using AutoMapper;
using E_Commerce.Dtos;
using E_Commerece.Core.Models;

namespace E_Commerce.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return null;

            var baseUrl = _configuration["BaseUrl"];
            return $"{baseUrl}{source.PictureUrl}";
        }
    }
}
