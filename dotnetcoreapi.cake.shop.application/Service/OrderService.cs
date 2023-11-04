using AutoMapper;
using dotnetcoreapi.cake.shop.domain;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoreapi.cake.shop.application
{
    public class OrderService : BaseService<Order, OrderDto, OrderRequestDto, OrderRequestDto>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IShippingMethodRepository _shippingMethodRepository;

        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IShippingMethodRepository shippingMethodRepository,
            IMapper mapper) : base(orderRepository, mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _shippingMethodRepository = shippingMethodRepository;
        }


        // Get all orders response DTO
        public async Task<ResponseDto> FilterAsync(
            int? status = null,
            int? pageSize = null,
            int? page = null,
            string? sort = null,
            string? search = null)
        {
            var allOrdersQuery = _orderRepository.GetAllEntities();

            // Status filter
            if(status.HasValue)
            {
                allOrdersQuery = allOrdersQuery.Where(o => o.OrderStatusId == status.Value);
            }

            // Search by customer name
            if(!string.IsNullOrEmpty(search))
            {
                allOrdersQuery = allOrdersQuery.Where(o => o.CustomerName.ToLower().Contains(search.ToLower()));
            }

            // Sort
            sort ??= "creationTimeDesc";
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "nameAsc":
                        allOrdersQuery = allOrdersQuery.OrderBy(p => p.CustomerName);
                        break;
                    case "nameDesc":
                        allOrdersQuery = allOrdersQuery.OrderByDescending(p => p.CustomerName);
                        break;
                    case "creationTimeAsc":
                        allOrdersQuery = allOrdersQuery.OrderBy(p => p.CreateAt);
                        break;
                    case "creationTimeDesc":
                        allOrdersQuery = allOrdersQuery.OrderByDescending(p => p.CreateAt);
                        break;
                    default:
                        break;
                }
            }

            // Paging
            int totalPage = 1;
            if (pageSize.HasValue && page.HasValue)
            {
                int totalItems = await allOrdersQuery.CountAsync();
                totalPage = (int)Math.Ceiling((double)totalItems / pageSize.Value);

                allOrdersQuery = allOrdersQuery.Skip((page.Value - 1) * pageSize.Value)
                                                   .Take(pageSize.Value);
            }

            var allOrders = await allOrdersQuery.ToListAsync();
            var allOrderResponseDtos = _mapper.Map<List<OrderDto>>(allOrders);

            return new ResponseDto()
            {
                Data = allOrderResponseDtos,
                TotalPage = totalPage,
            };
        }

        /// <summary>
        /// Map DTO sang entity để thêm bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Đối tượng cần map</param>
        /// <returns></returns>
        protected override async Task<Order> MapCreateAsync(OrderRequestDto orderRequestDto)
        {
            var newOrder = _mapper.Map<Order>(orderRequestDto);

            newOrder.CreateAt = DateTime.UtcNow;

            // Get order item price
            decimal totalItemsPrice = 0;

            if (newOrder.Items != null)
            {
                foreach (var orderItem in newOrder.Items)
                {
                    if (orderItem.ProductId.HasValue)
                    {
                        // Get origin product
                        var product = await _productRepository.GetEntityByIdAsync(orderItem.ProductId.Value);

                        orderItem.Price = product.Price;

                        totalItemsPrice += product.Price * orderItem.Qty;
                    }
                }
            }

            newOrder.OrderStatusId = (int)EOrderStatus.Created;

            // Get shipping method and shipping fee
            decimal shippingFee = 0;

            var defaultShippingMethod = (await _shippingMethodRepository.GetDefaultShippingMethodsAsync()).FirstOrDefault();
            if (defaultShippingMethod != null)
            {
                newOrder.ShippingMethodId = defaultShippingMethod.ShippingMethodId;

                // Example:
                // initCharge = 16.000 đ
                // initDistance = 2 km
                // additionalCharge = 5.500 đ (if distance > initDistance)
                // distance = 4 km
                // shippingCost = initCharge + (distance - initDistance) * additionalCharge
                // => shippingCost = 16.000 + (4 - 2) * 5.500
                if (orderRequestDto.Distance > defaultShippingMethod.InitialDistance)
                {
                    shippingFee = defaultShippingMethod.InitialCharge
                                  + (decimal)(Math.Ceiling(orderRequestDto.Distance) - defaultShippingMethod.InitialDistance) * defaultShippingMethod.AdditionalCharge;
                }
                else
                {
                    shippingFee = defaultShippingMethod.InitialCharge;
                }
            }

            newOrder.ShippingFee = shippingFee;

            // Get order total
            newOrder.OrderTotal = totalItemsPrice + shippingFee;

            return newOrder;
        }

        /// <summary>
        /// Map DTO sang entity để cập nhật bản ghi
        /// </summary>
        /// <param name="entityUpdateDto">Đối tượng cần map</param>
        /// <returns></returns>
        protected override async Task<Order> MapUpdateAsync(int entityId, OrderRequestDto entityUpdateDto)
        {
            var existOrder = await _orderRepository.GetEntityByIdAsync(entityId);

            _mapper.Map(entityUpdateDto, existOrder);

            return existOrder;
        }

        /// <summary>
        /// Chuyển trạng thái sang Đang vận chuyển
        /// </summary>
        /// <param name="orderId">ID đơn hàng</param>
        /// <returns></returns>
        public async Task<OrderDto> DeliveryOrder(int orderId)
        {
            var existOrder = await _orderRepository.GetEntityByIdAsync(orderId);

            existOrder.OrderStatusId = (int)EOrderStatus.Delivery;

            var updatedOrder = await _orderRepository.UpdateEntityAsync(existOrder);

            var updatedOrderResponseDto = _mapper.Map<OrderDto>(updatedOrder);
            return updatedOrderResponseDto;
        }

        /// <summary>
        /// Chuyển trạng thái sang Đã huỷ đơn hàng
        /// </summary>
        /// <param name="orderId">ID đơn hàng</param>
        /// <returns></returns>
        public async Task<OrderDto> CancelOrder(int orderId)
        {
            var existOrder = await _orderRepository.GetEntityByIdAsync(orderId);

            existOrder.OrderStatusId = (int)EOrderStatus.Cancelled;

            var cancelledOrder = await _orderRepository.UpdateEntityAsync(existOrder);

            var updatedOrderResponseDto = _mapper.Map<OrderDto>(cancelledOrder);
            return updatedOrderResponseDto;
        }

        /// <summary>
        /// Chuyển trạng thái sang Đã hoàn thành
        /// </summary>
        /// <param name="orderId">ID đơn hàng</param>
        /// <returns></returns>
        public async Task<OrderDto> SuccessOrder(int orderId)
        {
            var existOrder = await _orderRepository.GetEntityByIdAsync(orderId);

            existOrder.OrderStatusId = (int)EOrderStatus.Completed;

            var completedOrder = await _orderRepository.UpdateEntityAsync(existOrder);

            var updatedOrderResponseDto = _mapper.Map<OrderDto>(completedOrder);
            return updatedOrderResponseDto;
        }
    }
}
