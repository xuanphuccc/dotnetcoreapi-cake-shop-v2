using dotnetcoreapi_cake_shop.Entities;

namespace dotnetcoreapi_cake_shop.Repositories
{
    public interface IProductImageRepository
    {
        // Create product image
        Task<ProductImage> CreateProductImage(ProductImage productImage);

        // Delete product image
        Task<ProductImage> DeleteProductImage(ProductImage productImage);
    }
}
