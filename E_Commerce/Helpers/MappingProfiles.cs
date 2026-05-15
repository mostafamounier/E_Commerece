using AutoMapper;
using E_Commerce.Dtos;
using E_Commerece.Core.Models;
using E_Commerece.Core.Models.Identity;
using E_Commerece.Core.Models.Order_Aggreation;

namespace E_Commerce.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<E_Commerece.Core.Models.Identity.Address, AddressDto>().ReverseMap();

            CreateMap<Order, OrderDto>();
            
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<Order, OrderReturnDto>().ForMember(O => O.Cost, d => d.MapFrom(o => o.deliveryMethod.Cost)).
                ForMember(O => O.ShortName, d => d.MapFrom(o => o.deliveryMethod.ShortName));
            CreateMap<OrderItem, OrderItemDto>().ForMember(O => O.ProductId, d => d.MapFrom(o => o.Product.ProductId)).
                ForMember(O => O.ProductName, d => d.MapFrom(o => o.Product.ProductName)).
                ForMember(O => O.PictureUrl, d => d.MapFrom(o => o.Product.PictureUrl)).
                ForMember(O =>O.PictureUrl,d=>d.MapFrom<OrderItemPictureUrl>());




        }

    }
}
