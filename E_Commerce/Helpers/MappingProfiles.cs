using AutoMapper;
using E_Commerce.Dtos;
using E_Commerece.Core.Models;
using E_Commerece.Core.Models.Identity;

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
            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>();
        }

    }
}
