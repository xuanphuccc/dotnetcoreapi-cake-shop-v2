
namespace dotnetcoreapi.cake.shop.domain
{
    public interface IProductImageRepository
    {
        // Create product image
        Task<ProductImage> CreateProductImage(ProductImage productImage);

        // Delete product image
        Task<ProductImage> DeleteProductImage(ProductImage productImage);
    }
}
