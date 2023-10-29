using dotnetcoreapi_cake_shop.Dtos;
using dotnetcoreapi_cake_shop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcoreapi_cake_shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] int? limit)
        {
            var allCategoryResponseDtos = await _categoryService.GetAllCategories(limit);

            return Ok(new ResponseDto()
            {
                Data = allCategoryResponseDtos
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "categoryId is required" });
            }

            var categoryResponseDto = await _categoryService.GetCategoryById(id.Value);
            if (categoryResponseDto == null)
            {
                return NotFound(new ResponseDto() { Status = 404, Title = "category not found" });
            }

            return Ok(new ResponseDto()
            {
                Data = categoryResponseDto
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequestDto categoryRequestDto)
        {
            if (categoryRequestDto == null)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "category is required" });
            }

            try
            {
                var createdCategoryResponseDto = await _categoryService.CreateCategory(categoryRequestDto);

                return CreatedAtAction(
                    nameof(GetCategoryById),
                    new { id = createdCategoryResponseDto.CategoryId },
                    new ResponseDto()
                    {
                        Data = createdCategoryResponseDto,
                        Status = 201,
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseDto() { Status = 500, Title = ex.Message }
                );
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int? id, [FromBody] CategoryRequestDto categoryRequestDto)
        {
            if (!id.HasValue || categoryRequestDto == null)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "categoryId is required" });
            }

            try
            {
                var updatedCategoryResponseDto = await _categoryService.UpdateCategory(id.Value, categoryRequestDto);

                return Ok(new ResponseDto()
                {
                    Data = updatedCategoryResponseDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseDto() { Status = 500, Title = ex.Message }
                );
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "categoryId is required" });
            }

            try
            {
                var deletedCategoryResponseDto = await _categoryService.DeleteCategory(id.Value);

                return Ok(new ResponseDto()
                {
                    Data = deletedCategoryResponseDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseDto() { Status = 500, Title = ex.Message }
                );
            }
        }
    }
}
