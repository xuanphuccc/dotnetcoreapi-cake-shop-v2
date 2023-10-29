
namespace dotnetcoreapi.cake.shop.domain
{
    public interface IShippingMethodRepository
    {
        // Get all shipping methods
        IQueryable<ShippingMethod> GetAllShippingMethods();

        // Get default shipping methods
        Task<List<ShippingMethod>> GetDefaultShippingMethods();

        // Get shipping method by id
        Task<ShippingMethod> GetShippingMethodById(int shippingMethodId);

        // Create shipping method
        Task<ShippingMethod> CreateShippingMethod(ShippingMethod shippingMethod);

        // Update shipping method
        Task<ShippingMethod> UpdateShippingMethod(ShippingMethod shippingMethod);
        Task<List<ShippingMethod>> UpdateShippingMethods(List<ShippingMethod> shippingMethods);

        // Delete shipping method
        Task<ShippingMethod> DeleteShippingMethod(ShippingMethod shippingMethod);
    }
}
