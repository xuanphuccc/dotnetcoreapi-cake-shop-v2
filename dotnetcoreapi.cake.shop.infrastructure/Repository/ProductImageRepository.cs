using dotnetcoreapi.cake.shop.domain;

namespace dotnetcoreapi.cake.shop.infrastructure
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly CakeShopContext _context;
        public ProductImageRepository(CakeShopContext context)
        {
            _context = context;
        }

        // Create product image
        public async Task<ProductImage> CreateProductImage(ProductImage productImage)
        {
            await _context.ProductImages.AddAsync(productImage);
            var result = await _context.SaveChangesAsync();

            if(result == 0)
            {
                throw new Exception("cannot create product image");
            }

            return productImage;
        }

        // Delete product image
        public async Task<ProductImage> DeleteProductImage(ProductImage productImage)
        {
            _context.ProductImages.Remove(productImage);
            var result = await _context.SaveChangesAsync();

            if(result == 0)
            {
                throw new Exception("cannot delete product image");
            }

            return productImage;
        }
    }
}
