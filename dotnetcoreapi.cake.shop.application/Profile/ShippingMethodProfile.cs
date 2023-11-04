using AutoMapper;
using dotnetcoreapi.cake.shop.domain;

namespace dotnetcoreapi.cake.shop.application
{
    public class ShippingMethodProfile : Profile
    {
        public ShippingMethodProfile()
        {
            CreateMap<ShippingMethod, ShippingMethodDto>();
            CreateMap<ShippingMethodRequestDto, ShippingMethod>()
                .ForMember(
                    dest => dest.CreateAt,
                    opt => opt.Ignore()
                );
        }
    }
}
