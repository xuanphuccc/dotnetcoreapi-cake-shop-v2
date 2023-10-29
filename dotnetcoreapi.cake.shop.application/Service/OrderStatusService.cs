using AutoMapper;
using dotnetcoreapi.cake.shop.domain;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoreapi.cake.shop.application
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IOrderStatusRepository _orderStatusRepository;
        private readonly IMapper _mapper;
        public OrderStatusService(
            IOrderStatusRepository orderStatusRepository,
            IMapper mapper)
        {
            _orderStatusRepository = orderStatusRepository;
            _mapper = mapper;
        }

        // Get all order statuses response DTO
        public async Task<List<OrderStatusResponseDto>> GetAllOrderStatuses()
        {
            var orderStatuses = await _orderStatusRepository.GetAllOrderStatuses().ToListAsync();

            var orderStatusesResponseDto = _mapper.Map<List<OrderStatusResponseDto>>(orderStatuses);

            return orderStatusesResponseDto;
        }
    }
}
