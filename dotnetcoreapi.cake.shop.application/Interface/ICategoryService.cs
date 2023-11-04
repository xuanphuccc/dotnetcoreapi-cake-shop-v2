
namespace dotnetcoreapi.cake.shop.application
{
    public interface ICategoryService : IBaseService<CategoryDto, CategoryRequestDto, CategoryRequestDto>
    {
        /// <summary>
        /// Filter
        /// </summary>
        /// <param name="limit">Giới hạn bản ghi</param>
        /// <returns></returns>
        Task<List<CategoryDto>> FilterAsync(int? limit = null);
    }
}
