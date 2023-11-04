
namespace dotnetcoreapi.cake.shop.application
{
    public interface IShippingMethodService : IBaseService<ShippingMethodDto, ShippingMethodRequestDto, ShippingMethodRequestDto>
    {
        /// <summary>
        /// Lấy đơn vị vận chuyển mặc định
        /// </summary>
        /// <returns></returns>
        Task<ShippingMethodDto> GetDefaultShippingMethod();
    }
}
