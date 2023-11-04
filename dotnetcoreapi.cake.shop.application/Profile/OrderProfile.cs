using AutoMapper;
using dotnetcoreapi.cake.shop.domain;

namespace dotnetcoreapi.cake.shop.application
{
    public class OrderProfile : Profile
    {
        public OrderProfile() {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();

            CreateMap<OrderRequestDto, Order>()
                .ForMember(
                    dest => dest.CreateAt, opt => opt.Ignore()
                );
            CreateMap<OrderItemRequestDto, OrderItem>();
        }
    }
}
