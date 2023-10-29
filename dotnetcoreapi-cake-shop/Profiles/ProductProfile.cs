using AutoMapper;
using dotnetcoreapi_cake_shop.Dtos;
using dotnetcoreapi_cake_shop.Entities;

namespace dotnetcoreapi_cake_shop.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponseDto>();
            CreateMap<ProductRequestDto, Product>()
                .ForMember(
                    dest => dest.CreateAt,
                    opt => opt.Ignore()
                 );
            CreateMap<ProductImage, ProductImageResponseDto>();
            CreateMap<ProductImageRequestDto, ProductImage>();
        }
    }
}
