
namespace dotnetcoreapi.cake.shop.domain
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        /// <summary>
        /// Kiểm tra một sản phẩm đã từng được bán chưa
        /// </summary>
        /// <param name="productId">ID sản phẩm</param>
        /// <returns></returns>
        Task<int> HasOrders(int productId);
    }
}
