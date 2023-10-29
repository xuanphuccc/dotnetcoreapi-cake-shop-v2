using AutoMapper;
using dotnetcoreapi_cake_shop.Dtos;
using dotnetcoreapi_cake_shop.Entities;

namespace dotnetcoreapi_cake_shop.Profiles
{
    public class ShippingMethodProfile : Profile
    {
        public ShippingMethodProfile()
        {
            CreateMap<ShippingMethod, ShippingMethodResponseDto>();
            CreateMap<ShippingMethodRequestDto, ShippingMethod>()
                .ForMember(
                    dest => dest.CreateAt,
                    opt => opt.Ignore()
                );
        }
    }
}
