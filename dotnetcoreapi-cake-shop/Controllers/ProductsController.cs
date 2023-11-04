using dotnetcoreapi.cake.shop.application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcoreapi.cake.shop
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductsController(
            IProductService productService,
            ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(
            [FromQuery] int? category,
            [FromQuery] int? pageSize,
            [FromQuery] int? page,
            [FromQuery] string? sort,
            [FromQuery] string? search)
        {
            var allProductResponseDtos = await _productService.FilterAsync(category, pageSize, page, sort, search);

            return Ok(allProductResponseDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "productId is required" });
            }

            var productResponseDto = await _productService.GetEntityByIdAsync(id.Value);
            if (productResponseDto == null)
            {
                return NotFound(new ResponseDto() { Status = 404, Title = "product not found" });
            }

            return Ok(new ResponseDto()
            {
                Data = productResponseDto
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequestDto productRequestDto)
        {
            if (productRequestDto == null)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "product is required" });
            }

            // Check exist category
            if (productRequestDto.CategoryId != null)
            {
                var existCategory = await _categoryService.GetEntityByIdAsync(productRequestDto.CategoryId.Value);
                if (existCategory == null)
                {
                    return BadRequest(new ResponseDto() { Status = 400, Title = "category not found" });
                }
            }

            try
            {
                // Create product
                var createdProductResponseDto = await _productService.CreateEntityAsync(productRequestDto);

                return CreatedAtAction(
                    nameof(GetProductById),
                    new { id = createdProductResponseDto.ProductId },
                    new ResponseDto()
                    {
                        Data = createdProductResponseDto,
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
        public async Task<IActionResult> UpdateProduct([FromRoute] int? id, [FromBody] ProductRequestDto productRequestDto)
        {
            if (!id.HasValue || productRequestDto == null)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "productId is required" });
            }

            // Check exist category
            if (productRequestDto.CategoryId != null)
            {
                var existCategory = await _categoryService.GetEntityByIdAsync(productRequestDto.CategoryId.Value);
                if (existCategory == null)
                {
                    return BadRequest(new ResponseDto() { Status = 400, Title = "category not found" });
                }
            }

            try
            {
                // Update product
                var updatedProductResponseDto = await _productService.UpdateEntityAsync(id.Value, productRequestDto);

                return Ok(new ResponseDto()
                {
                    Data = updatedProductResponseDto
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
        public async Task<IActionResult> DeleteProduct([FromRoute] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "productId is required" });
            }

            try
            {
                // Delete product
                var deletedProductResponseDto = await _productService.DeleteEntityAsync(id.Value);

                return Ok(new ResponseDto()
                {
                    Data = deletedProductResponseDto
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
