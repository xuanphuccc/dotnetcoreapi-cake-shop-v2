using AutoMapper;
using dotnetcoreapi_cake_shop.Dtos;
using dotnetcoreapi_cake_shop.Entities;
using dotnetcoreapi_cake_shop.Repositories;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoreapi_cake_shop.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        // Get all categories response DTO
        public async Task<List<CategoryResponseDto>> GetAllCategories(int? limit = null)
        {
            var allCategoriesQuery = _categoryRepository.GetAllCategories();

            // Get limit categories
            if(limit.HasValue)
            {
                allCategoriesQuery = allCategoriesQuery.Take(limit.Value);
            }

            var allCategories = await allCategoriesQuery.ToListAsync();
            var allCategoryResponseDtos = _mapper.Map<List<CategoryResponseDto>>(allCategories);

            return allCategoryResponseDtos;
        }

        // Get category response DTO
        public async Task<CategoryResponseDto> GetCategoryById(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryById(categoryId);

            var categoryResponseDto = _mapper.Map<CategoryResponseDto>(category);
            return categoryResponseDto;
        }

        // Create category
        public async Task<CategoryResponseDto> CreateCategory(CategoryRequestDto categoryRequestDto)
        {
            var newCategory = _mapper.Map<Category>(categoryRequestDto);
            newCategory.CreateAt = DateTime.UtcNow;

            var createdCategory = await _categoryRepository.CreateCategory(newCategory);

            var createdCategoryResponseDto = _mapper.Map<CategoryResponseDto>(createdCategory);
            return createdCategoryResponseDto;
        }

        // Update category
        public async Task<CategoryResponseDto> UpdateCategory(int id, CategoryRequestDto categoryRequestDto)
        {
            var existCategory = await _categoryRepository.GetCategoryById(id);

            if (existCategory == null)
            {
                throw new Exception("category not found");
            }

            _mapper.Map(categoryRequestDto, existCategory);
            var updatedCategory = await _categoryRepository.UpdateCategory(existCategory);

            var updatedCategoryResponseDto = _mapper.Map<CategoryResponseDto>(updatedCategory);
            return updatedCategoryResponseDto;
        }

        // Delete category
        public async Task<CategoryResponseDto> DeleteCategory(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryById(categoryId);

            if (category == null)
            {
                throw new Exception("category not found");
            }

            var deletedCategory = await _categoryRepository.DeleteCategory(category);

            var deletedCategoryResponseDto = _mapper.Map<CategoryResponseDto>(deletedCategory);
            return deletedCategoryResponseDto;
        }
    }
}
