
namespace dotnetcoreapi.cake.shop.domain
{
    public interface IProductRepository
    {
        // Get all products
        IQueryable<Product> GetAllProducts();

        // Get product by id
        Task<Product> GetProductById(int productId);

        // Create product
        Task<Product> CreateProduct(Product product);

        // Update product
        Task<Product> UpdateProduct(Product product);

        // Delete product
        Task<Product> DeleteProduct(Product product);
    }
}
