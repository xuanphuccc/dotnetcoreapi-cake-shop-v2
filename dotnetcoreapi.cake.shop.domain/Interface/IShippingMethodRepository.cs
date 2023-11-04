
namespace dotnetcoreapi.cake.shop.domain
{
    public interface IShippingMethodRepository : IBaseRepository<ShippingMethod>
    {
        /// <summary>
        /// Lấy đơn vị vận chuyển mặc định
        /// </summary>
        /// <returns></returns>
        Task<List<ShippingMethod>> GetDefaultShippingMethodsAsync();

        /// <summary>
        /// Cập nhật danh sách đơn vị vận chuyển
        /// </summary>
        /// <param name="shippingMethods">Danh sách bản ghi cần cập nhật</param>
        /// <returns></returns>
        Task<List<ShippingMethod>> UpdateShippingMethodsAsync(List<ShippingMethod> shippingMethods);
    }
}
