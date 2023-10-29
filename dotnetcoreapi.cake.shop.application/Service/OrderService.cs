using AutoMapper;
using dotnetcoreapi.cake.shop.domain;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoreapi.cake.shop.application
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IShippingMethodRepository _shippingMethodRepository;
        private readonly IOrderStatusRepository _orderStatusRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderStatusRepository orderStatusRepository,
            IProductRepository productRepository,
            IShippingMethodRepository shippingMethodRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _shippingMethodRepository = shippingMethodRepository;
            _orderStatusRepository = orderStatusRepository;
            _mapper = mapper;
        }


        // Get all orders response DTO
        public async Task<ResponseDto> GetAllOrders(
            int? status = null,
            int? pageSize = null,
            int? page = null,
            string? sort = null,
            string? search = null)
        {
            var allOrdersQuery = _orderRepository.GetAllOrders();

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
            var allOrderResponseDtos = _mapper.Map<List<OrderResponseDto>>(allOrders);

            return new ResponseDto()
            {
                Data = allOrderResponseDtos,
                TotalPage = totalPage,
            };
        }

        // Get order response DTO
        public async Task<OrderResponseDto> GetOrderById(int orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);

            var orderResponseDto = _mapper.Map<OrderResponseDto>(order);
            return orderResponseDto;
        }

        // Create order
        public async Task<OrderResponseDto> CreateOrder(OrderRequestDto orderRequestDto)
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
                        var product = await _productRepository.GetProductById(orderItem.ProductId.Value);

                        orderItem.Price = product.Price;

                        totalItemsPrice += product.Price * orderItem.Qty;
                    }
                }
            }

            // Get order status
            OrderStatus createdStatus = await _orderStatusRepository.GetOrderStatusByStatus("created");

            if (createdStatus == null)
            {
                // If 'created' status does not exist -> create new 'created' status
                createdStatus = await CreateOrderStatus("Đã tạo đơn hàng", "created");
            }

            newOrder.OrderStatusId = createdStatus.OrderStatusId;

            // Get shipping method and shipping fee
            decimal shippingFee = 0;

            var defaultShippingMethod = (await _shippingMethodRepository
                                                .GetDefaultShippingMethods())
                                                .FirstOrDefault();
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

            // Create order
            var createdOrder = await _orderRepository.CreateOrder(newOrder);

            var createdOrderResponseDto = _mapper.Map<OrderResponseDto>(createdOrder);
            return createdOrderResponseDto;
        }

        // Update order status
        // Delivery
        public async Task<OrderResponseDto> DeliveryOrder(int orderId)
        {
            var existOrder = await _orderRepository.GetOrderById(orderId);

            if (existOrder == null)
            {

                throw new Exception("order not found");
            }

            // Get order status
            OrderStatus deliveryStatus = await _orderStatusRepository.GetOrderStatusByStatus("delivery");

            if (deliveryStatus == null)
            {
                // If 'delivery' status does not exist -> create new 'delivery' status
                deliveryStatus = await CreateOrderStatus("Đang giao hàng", "delivery");
            }

            // Update new order status
            existOrder.OrderStatusId = deliveryStatus.OrderStatusId;
            var updatedOrder = await _orderRepository.UpdateOrder(existOrder);

            var updatedOrderResponseDto = _mapper.Map<OrderResponseDto>(updatedOrder);
            return updatedOrderResponseDto;
        }

        // Cancelled
        public async Task<OrderResponseDto> CancelOrder(int orderId)
        {
            var existOrder = await _orderRepository.GetOrderById(orderId);

            if (existOrder == null)
            {

                throw new Exception("order not found");
            }

            // Get order status
            OrderStatus deliveryStatus = await _orderStatusRepository.GetOrderStatusByStatus("cancelled");

            if (deliveryStatus == null)
            {
                // If 'cancelled' status does not exist -> create new 'cancelled' status
                deliveryStatus = await CreateOrderStatus("Đã huỷ đơn hàng", "cancelled");
            }

            // Update new order status
            existOrder.OrderStatusId = deliveryStatus.OrderStatusId;
            var cancelledOrder = await _orderRepository.UpdateOrder(existOrder);

            var updatedOrderResponseDto = _mapper.Map<OrderResponseDto>(cancelledOrder);
            return updatedOrderResponseDto;
        }

        // Completed
        public async Task<OrderResponseDto> SuccessOrder(int orderId)
        {
            var existOrder = await _orderRepository.GetOrderById(orderId);

            if (existOrder == null)
            {

                throw new Exception("order not found");
            }

            // Get order status
            OrderStatus deliveryStatus = await _orderStatusRepository.GetOrderStatusByStatus("completed");

            if (deliveryStatus == null)
            {
                // If 'completed' status does not exist -> create new 'completed' status
                deliveryStatus = await CreateOrderStatus("Đã hoàn thành", "completed");
            }

            // Update new order status
            existOrder.OrderStatusId = deliveryStatus.OrderStatusId;
            var completedOrder = await _orderRepository.UpdateOrder(existOrder);

            var updatedOrderResponseDto = _mapper.Map<OrderResponseDto>(completedOrder);
            return updatedOrderResponseDto;
        }

        // Delete order
        public async Task<OrderResponseDto> DeleteOrder(int orderId)
        {
            var existOrder = await _orderRepository.GetOrderById(orderId);

            if (existOrder == null)
            {
                throw new Exception("order not found");
            }

            var deletedOrder = await _orderRepository.DeleteOrder(existOrder);

            var deletedOrderResponseDto = _mapper.Map<OrderResponseDto>(deletedOrder);
            return deletedOrderResponseDto;
        }


        // Create order status
        private async Task<OrderStatus> CreateOrderStatus(string name, string status)
        {
            var newStatus = new OrderStatus()
            {
                Name = name,
                Status = status.ToLower(),
            };

            var createdOrderStatus = await _orderStatusRepository.CreateOrderStatus(newStatus);

            return createdOrderStatus;
        }
    }
}
