using dotnetcoreapi_cake_shop.Dtos;

namespace dotnetcoreapi_cake_shop.Services
{
    public interface ICategoryService
    {
        // Get all categories response DTO
        Task<List<CategoryResponseDto>> GetAllCategories(int? limit = null);

        // Get category response DTO
        Task<CategoryResponseDto> GetCategoryById(int categoryId);

        // Create category
        Task<CategoryResponseDto> CreateCategory(CategoryRequestDto categoryRequestDto);

        // Update category
        Task<CategoryResponseDto> UpdateCategory(int id, CategoryRequestDto categoryRequestDto);

        // Delete category
        Task<CategoryResponseDto> DeleteCategory(int categoryId);
    }
}
