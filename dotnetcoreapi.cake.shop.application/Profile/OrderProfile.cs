using AutoMapper;
using dotnetcoreapi.cake.shop.domain;

namespace dotnetcoreapi.cake.shop.application
{
    public class OrderProfile : Profile
    {
        public OrderProfile() {
            CreateMap<Order, OrderResponseDto>();
            CreateMap<OrderItem, OrderItemResponseDto>();
            CreateMap<OrderStatus, OrderStatusResponseDto>();

            CreateMap<OrderRequestDto, Order>()
                .ForMember(
                    dest => dest.CreateAt, opt => opt.Ignore()
                );
            CreateMap<OrderItemRequestDto, OrderItem>();
        }
    }
}
