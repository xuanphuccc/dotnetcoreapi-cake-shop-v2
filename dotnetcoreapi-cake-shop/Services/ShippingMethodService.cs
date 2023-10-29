using AutoMapper;
using dotnetcoreapi_cake_shop.Dtos;
using dotnetcoreapi_cake_shop.Entities;
using dotnetcoreapi_cake_shop.Repositories;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoreapi_cake_shop.Services
{
    public class ShippingMethodService : IShippingMethodService
    {
        private readonly IShippingMethodRepository _shippingMethodRepository;
        private readonly IMapper _mapper;
        public ShippingMethodService(
            IShippingMethodRepository shippingMethodRepository,
            IMapper mapper)
        {
            _shippingMethodRepository = shippingMethodRepository;
            _mapper = mapper;
        }


        // Get all shipping methods
        public async Task<List<ShippingMethodResponseDto>> GetAllShippingMethods()
        {
            var allShippingMethods = await _shippingMethodRepository
                                            .GetAllShippingMethods()
                                            .ToListAsync();

            var allShippingMethodResponseDto = _mapper.Map<List<ShippingMethodResponseDto>>(allShippingMethods);
            return allShippingMethodResponseDto;
        }

        // Get default shipping method
        public async Task<ShippingMethodResponseDto> GetDefaultShippingMethod()
        {
            var defaultShippingMethod = (await _shippingMethodRepository
                                        .GetDefaultShippingMethods()).FirstOrDefault();

            var defaultShippingMethodResponseDto = _mapper.Map<ShippingMethodResponseDto>(defaultShippingMethod);
            return defaultShippingMethodResponseDto;
        }

        // Get shipping method by ID
        public async Task<ShippingMethodResponseDto> GetShippingMethodById(int shippingMethodId)
        {
            var shippingMethod = await _shippingMethodRepository.GetShippingMethodById(shippingMethodId);

            var shippingMethodResponseDto = _mapper.Map<ShippingMethodResponseDto>(shippingMethod);
            return shippingMethodResponseDto;
        }

        // Create shipping method
        public async Task<ShippingMethodResponseDto> CreateShippingMethod(ShippingMethodRequestDto shippingMethodRequestDto)
        {
            var newShippingMethod = _mapper.Map<ShippingMethod>(shippingMethodRequestDto);
            newShippingMethod.CreateAt = DateTime.UtcNow;

            if (newShippingMethod.IsDefault == true)
            {
                // Update old default shipping methods to 'false'
                await UpdateOldDefaultShippingMethods();
            }

            var createdShippingMethod = await _shippingMethodRepository.CreateShippingMethod(newShippingMethod);

            var createdShippingMethodResponseDto = _mapper.Map<ShippingMethodResponseDto>(createdShippingMethod);
            return createdShippingMethodResponseDto;
        }

        // Update shipping method
        public async Task<ShippingMethodResponseDto> UpdateShippingMethod(int id, ShippingMethodRequestDto shippingMethodRequestDto)
        {
            var existShippingMethod = await _shippingMethodRepository.GetShippingMethodById(id);

            if (existShippingMethod == null)
            {
                throw new Exception("shipping method not found");
            }

            _mapper.Map(shippingMethodRequestDto, existShippingMethod);

            if (existShippingMethod.IsDefault == true)
            {
                // Update old default shipping methods to 'false'
                await UpdateOldDefaultShippingMethods();
            }

            var updatedShippingMethod = await _shippingMethodRepository.UpdateShippingMethod(existShippingMethod);

            var updatedShippingMethodResponseDto = _mapper.Map<ShippingMethodResponseDto>(updatedShippingMethod);
            return updatedShippingMethodResponseDto;
        }

        // Delete shipping method
        public async Task<ShippingMethodResponseDto> DeleteShippingMethod(int shippingMethodId)
        {
            var existShippingMethod = await _shippingMethodRepository.GetShippingMethodById(shippingMethodId);

            if (existShippingMethod == null)
            {
                throw new Exception("shipping method not found");
            }

            var deletedShippingMethod = await _shippingMethodRepository.DeleteShippingMethod(existShippingMethod);

            // Check exist default shipping method
            if (deletedShippingMethod.IsDefault == true)
            {
                await ChangeDefaultShippingMethod();
            }

            var deletedShippingMethodResponseDto = _mapper.Map<ShippingMethodResponseDto>(deletedShippingMethod);
            return deletedShippingMethodResponseDto;
        }


        // Update old default shipping methods to 'false'
        private async Task UpdateOldDefaultShippingMethods()
        {
            var defaultShippingMethods = await _shippingMethodRepository.GetDefaultShippingMethods();

            defaultShippingMethods.ForEach(s =>
            {
                s.IsDefault = false;
            });

            await _shippingMethodRepository.UpdateShippingMethods(defaultShippingMethods);
        }

        // Change default shipping method
        private async Task ChangeDefaultShippingMethod()
        {
            var firstShippingMethod = await _shippingMethodRepository
                                            .GetAllShippingMethods()
                                            .FirstOrDefaultAsync();

            if (firstShippingMethod != null)
            {
                firstShippingMethod.IsDefault = true;

                await _shippingMethodRepository.UpdateShippingMethod(firstShippingMethod);
            }
        }
    }
}
