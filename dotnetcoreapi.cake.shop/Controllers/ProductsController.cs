using dotnetcoreapi.cake.shop.application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcoreapi.cake.shop
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController<ProductDto, ProductRequestDto, ProductRequestDto>
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService) : base(productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Tìm kiếm, phân trang, sắp xếp
        /// </summary>
        /// <param name="category"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public async Task<IActionResult> FilterAsync(
            [FromQuery] int? category,
            [FromQuery] int? pageSize,
            [FromQuery] int? page,
            [FromQuery] string? sort,
            [FromQuery] string? search)
        {
            var allProductResponseDtos = await _productService.FilterAsync(category, pageSize, page, sort, search);

            return Ok(allProductResponseDtos);
        }
    }
}
