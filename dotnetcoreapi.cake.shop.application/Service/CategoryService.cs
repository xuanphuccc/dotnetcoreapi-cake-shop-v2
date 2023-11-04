using AutoMapper;
using dotnetcoreapi.cake.shop.domain;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoreapi.cake.shop.application
{
    public class CategoryService : BaseService<Category, CategoryDto, CategoryRequestDto, CategoryRequestDto>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : base(categoryRepository, mapper)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Filter
        /// </summary>
        /// <param name="limit">Giới hạn bản ghi</param>
        /// <returns></returns>
        public async Task<List<CategoryDto>> FilterAsync(int? limit = null)
        {
            var allCategoriesQuery = _categoryRepository.GetAllEntities();

            // Get limit categories
            if (limit.HasValue)
            {
                allCategoriesQuery = allCategoriesQuery.Take(limit.Value);
            }

            var allCategories = await allCategoriesQuery.ToListAsync();
            var allCategoryResponseDtos = _mapper.Map<List<CategoryDto>>(allCategories);

            return allCategoryResponseDtos;
        }

        /// <summary>
        /// Map DTO sang entity để thêm bản ghi
        /// </summary>
        /// <param name="categoryRequestDto">Đối tượng cần map</param>
        /// <returns></returns>
        protected override async Task<Category> MapCreateAsync(CategoryRequestDto categoryRequestDto)
        {
            var newCategory = _mapper.Map<Category>(categoryRequestDto);
            newCategory.CreateAt = DateTime.UtcNow;

            return await Task.FromResult(newCategory);
        }

        /// <summary>
        /// Map DTO sang entity để cập nhật bản ghi
        /// </summary>
        /// <param name="entityUpdateDto">Đối tượng cần map</param>
        /// <returns></returns>
        protected override async Task<Category> MapUpdateAsync(int entityId, CategoryRequestDto entityUpdateDto)
        {
            var existCategory = await _categoryRepository.GetEntityByIdAsync(entityId);

            _mapper.Map(entityUpdateDto, existCategory);

            return existCategory;
        }
    }
}
