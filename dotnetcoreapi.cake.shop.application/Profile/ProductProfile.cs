using AutoMapper;
using dotnetcoreapi.cake.shop.domain;

namespace dotnetcoreapi.cake.shop.application
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductRequestDto, Product>()
                .ForMember(
                    dest => dest.CreateAt,
                    opt => opt.Ignore()
                 );
            CreateMap<ProductImage, ProductImageDto>();
            CreateMap<ProductImageRequestDto, ProductImage>();
        }
    }
}
