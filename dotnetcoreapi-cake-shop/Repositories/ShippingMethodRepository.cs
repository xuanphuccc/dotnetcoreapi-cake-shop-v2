using dotnetcoreapi_cake_shop.Data;
using dotnetcoreapi_cake_shop.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoreapi_cake_shop.Repositories
{
    public class ShippingMethodRepository : IShippingMethodRepository
    {
        private readonly CakeShopContext _context;
        public ShippingMethodRepository(CakeShopContext context)
        {
            _context = context;
        }

        // Get all shipping methods
        public IQueryable<ShippingMethod> GetAllShippingMethods()
        {
            var allShippingMethods = _context.ShippingMethods.AsQueryable();
            return allShippingMethods;
        }

        // Get default shipping methods
        public async Task<List<ShippingMethod>> GetDefaultShippingMethods()
        {
            var defaultShippingMethods = await _context.ShippingMethods
                                                .Where(s => s.IsDefault == true)
                                                .ToListAsync();

            return defaultShippingMethods;
        }

        // Get shipping method by id
        public async Task<ShippingMethod> GetShippingMethodById(int shippingMethodId)
        {
            var shippingMethod = await _context.ShippingMethods
                                .FirstOrDefaultAsync(s => s.ShippingMethodId == shippingMethodId);

            return shippingMethod!;
        }

        // Create shipping method
        public async Task<ShippingMethod> CreateShippingMethod(ShippingMethod shippingMethod)
        {
            await _context.ShippingMethods.AddAsync(shippingMethod);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("cannot create shipping method");
            }

            return shippingMethod;
        }

        // Update shipping method
        public async Task<ShippingMethod> UpdateShippingMethod(ShippingMethod shippingMethod)
        {
            _context.ShippingMethods.Update(shippingMethod);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("not modified");
            }

            return shippingMethod;
        }

        // Update shipping methods
        public async Task<List<ShippingMethod>> UpdateShippingMethods(List<ShippingMethod> shippingMethods)
        {
            _context.ShippingMethods.UpdateRange(shippingMethods);
            var result = await _context.SaveChangesAsync();

            if(result == 0)
            {
                throw new Exception("not modified");
            }

            return shippingMethods;
        }

        // Delete shipping method
        public async Task<ShippingMethod> DeleteShippingMethod(ShippingMethod shippingMethod)
        {
            _context.ShippingMethods.Remove(shippingMethod);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("cannot delete shipping method");
            }

            return shippingMethod;
        }
    }
}
