using AutoMapper;
using dotnetcoreapi.cake.shop.domain;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoreapi.cake.shop.application
{
    public class ShippingMethodService : BaseService<ShippingMethod, ShippingMethodDto, ShippingMethodRequestDto, ShippingMethodRequestDto>, IShippingMethodService
    {
        private readonly IShippingMethodRepository _shippingMethodRepository;

        public ShippingMethodService(IShippingMethodRepository shippingMethodRepository, IMapper mapper) : base(shippingMethodRepository, mapper)
        {
            _shippingMethodRepository = shippingMethodRepository;
        }

        /// <summary>
        /// Lấy đơn vị vận chuyển mặc định
        /// </summary>
        /// <returns></returns>
        public async Task<ShippingMethodDto> GetDefaultShippingMethod()
        {
            var defaultShippingMethod = (await _shippingMethodRepository
                                        .GetDefaultShippingMethodsAsync()).FirstOrDefault();

            var defaultShippingMethodResponseDto = _mapper.Map<ShippingMethodDto>(defaultShippingMethod);
            return defaultShippingMethodResponseDto;
        }

        /// <summary>
        /// Map DTO sang entity để thêm bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Đối tượng cần map</param>
        /// <returns></returns>
        protected override async Task<ShippingMethod> MapCreateAsync(ShippingMethodRequestDto entityCreateDto)
        {
            var newShippingMethod = _mapper.Map<ShippingMethod>(entityCreateDto);
            newShippingMethod.CreateAt = DateTime.UtcNow;

            if (newShippingMethod.IsDefault == true)
            {
                // Update old default shipping methods to 'false'
                await UpdateOldDefaultShippingMethods();
            }

            return newShippingMethod;
        }

        /// <summary>
        /// Map DTO sang entity để cập nhật bản ghi
        /// </summary>
        /// <param name="entityUpdateDto">Đối tượng cần map</param>
        /// <returns></returns>
        protected override async Task<ShippingMethod> MapUpdateAsync(int entityId, ShippingMethodRequestDto entityUpdateDto)
        {
            var existShippingMethod = await _shippingMethodRepository.GetEntityByIdAsync(entityId);

            _mapper.Map(entityUpdateDto, existShippingMethod);

            if (existShippingMethod.IsDefault == true)
            {
                // Update old default shipping methods to 'false'
                await UpdateOldDefaultShippingMethods();

                existShippingMethod.IsDefault = true;
            }


            return existShippingMethod;
        }

        /// <summary>
        /// Thực hiện hành động sau khi xoá
        /// </summary>
        /// <param name="deletedEntity">Đối tượng đã xoá</param>
        /// <returns></returns>
        protected override async Task AfterDeleteAsync(ShippingMethod deletedEntity)
        {
            // Nếu xoá phương thức vận chuyển mặc định
            // Thì chuyển phương thức vận chuyển đầu tiên thành mặc định
            if (deletedEntity.IsDefault == true)
            {
                await ChangeDefaultShippingMethod();
            }
        }

        /// <summary>
        /// Bỏ mặc định phương thức vận chuyển mặc định trước đó
        /// </summary>
        /// <returns></returns>
        private async Task UpdateOldDefaultShippingMethods()
        {
            var defaultShippingMethods = await _shippingMethodRepository.GetDefaultShippingMethodsAsync();

            defaultShippingMethods.ForEach(sm =>
            {
                sm.IsDefault = false;
            });

            await _shippingMethodRepository.UpdateShippingMethodsAsync(defaultShippingMethods);
        }

        /// <summary>
        /// Chuyển phương thức vận chuyển đầu tiên thành mặc định
        /// </summary>
        /// <returns></returns>
        private async Task ChangeDefaultShippingMethod()
        {
            var firstShippingMethod = await _shippingMethodRepository.GetAllEntities().FirstOrDefaultAsync();

            if (firstShippingMethod != null)
            {
                firstShippingMethod.IsDefault = true;

                await _shippingMethodRepository.UpdateEntityAsync(firstShippingMethod);
            }
        }
    }
}
