using dotnetcoreapi_cake_shop.Entities;

namespace dotnetcoreapi_cake_shop.Repositories
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
