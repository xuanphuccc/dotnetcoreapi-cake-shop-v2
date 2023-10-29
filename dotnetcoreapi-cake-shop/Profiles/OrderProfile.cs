using AutoMapper;
using dotnetcoreapi_cake_shop.Dtos;
using dotnetcoreapi_cake_shop.Entities;

namespace dotnetcoreapi_cake_shop.Profiles
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
