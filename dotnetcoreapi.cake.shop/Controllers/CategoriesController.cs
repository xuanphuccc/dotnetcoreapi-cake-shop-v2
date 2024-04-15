using dotnetcoreapi.cake.shop.application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcoreapi.cake.shop
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController<CategoryDto, CategoryRequestDto, CategoryRequestDto>
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService) : base(categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Filter
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public async Task<IActionResult> FilterAsync([FromQuery] int? limit)
        {
            var allCategoryResponseDtos = await _categoryService.FilterAsync(limit);

            return Ok(new ResponseDto() { Data = allCategoryResponseDtos });
        }
    }
}
