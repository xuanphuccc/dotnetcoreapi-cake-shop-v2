using dotnetcoreapi.cake.shop.domain;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoreapi.cake.shop.infrastructure
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CakeShopContext _context;
        public CategoryRepository( CakeShopContext context )
        {
            _context = context;
        }

        // Get all categories
        public IQueryable<Category> GetAllCategories()
        {
            var categories = _context.Categories.AsQueryable();
            return categories;
        }

        // Get category by id
        public async Task<Category> GetCategoryById(int categoryId)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
            return category!;
        }

        // Create category
        public async Task<Category> CreateCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            var result = await _context.SaveChangesAsync();

            if(result == 0)
            {
                throw new Exception("cannot create category");
            }

            return category;
        }

        // Update category
        public async Task<Category> UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("not modified");
            }

            return category;
        }

        // Delete category
        public async Task<Category> DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            var result = await _context.SaveChangesAsync();

            if(result == 0)
            {
                throw new Exception("cannot delete category");
            }

            return category;
        }
    }
}
