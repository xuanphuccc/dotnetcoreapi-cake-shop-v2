using dotnetcoreapi_cake_shop.Data;
using dotnetcoreapi_cake_shop.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoreapi_cake_shop.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CakeShopContext _context;
        public ProductRepository(CakeShopContext context)
        {
            _context = context;
        }

        // Get all products
        public IQueryable<Product> GetAllProducts()
        {
            var allProducts = _context.Products
                                .Include(p => p.Category)
                                .Include(p => p.Images)
                                .AsQueryable();

            return allProducts;
        }

        // Get product by id
        public async Task<Product> GetProductById(int productId)
        {
            var product = await _context.Products
                                .Include(p => p.Category)
                                .Include(p => p.Images)
                                .FirstOrDefaultAsync(p => p.ProductId == productId);

            return product!;
        }

        // Create product
        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("cannot create product");
            }

            return product;
        }

        // Update product
        public async Task<Product> UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("not modified");
            }

            return product;
        }

        // Delete product
        public async Task<Product> DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("cannot delete product");
            }

            return product;
        }
    }
}
