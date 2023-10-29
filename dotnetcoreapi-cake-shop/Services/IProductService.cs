using dotnetcoreapi_cake_shop.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcoreapi_cake_shop.Services
{
    public interface IProductService
    {
        // Get all products response DTO
        Task<ResponseDto> GetAllProducts(
            int? category = null, 
            int? pageSize = null, 
            int? page = null, 
            string? sort = null, 
            string? search = null);

        // Get product response DTO
        Task<ProductResponseDto> GetProductById(int productId);

        // Create product
        Task<ProductResponseDto> CreateProduct(ProductRequestDto productRequestDto);

        // Update product
        Task<ProductResponseDto> UpdateProduct(int id, ProductRequestDto productRequestDto);

        // Delete product
        Task<ProductResponseDto> DeleteProduct(int productId);
    }
}
