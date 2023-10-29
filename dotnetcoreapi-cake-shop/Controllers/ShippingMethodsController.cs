using dotnetcoreapi_cake_shop.Dtos;
using dotnetcoreapi_cake_shop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcoreapi_cake_shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingMethodsController : ControllerBase
    {
        private readonly IShippingMethodService _shippingMethodService;
        public ShippingMethodsController(IShippingMethodService shippingMethodService)
        {
            _shippingMethodService = shippingMethodService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShippingMethods()
        {
            var allShippingMethodResponseDtos = await _shippingMethodService.GetAllShippingMethods();

            return Ok(new ResponseDto()
            {
                Data = allShippingMethodResponseDtos
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShippingMethodById([FromRoute] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "shippingMethodId is required" });
            }

            var shippingMethodResponseDto = await _shippingMethodService.GetShippingMethodById(id.Value);
            if (shippingMethodResponseDto == null)
            {
                return NotFound(new ResponseDto() { Status = 404, Title = "shipping method not found" });
            }

            return Ok(new ResponseDto()
            {
                Data = shippingMethodResponseDto
            });
        }

        [HttpGet("default")]
        public async Task<IActionResult> GetDefaultShippingMethod()
        {
            var shippingMethodResponseDto = await _shippingMethodService.GetDefaultShippingMethod();
            if (shippingMethodResponseDto == null)
            {
                return NotFound(new ResponseDto() { Status = 404, Title = "shipping method not found" });
            }

            return Ok(new ResponseDto()
            {
                Data = shippingMethodResponseDto
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateShippingMethod([FromBody] ShippingMethodRequestDto shippingMethodRequestDto)
        {
            if (shippingMethodRequestDto == null)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "shippingMethod is required" });
            }

            try
            {
                var createdShippingMethodResponseDto = await _shippingMethodService.CreateShippingMethod(shippingMethodRequestDto);

                return CreatedAtAction(
                            nameof(GetShippingMethodById),
                            new { id = createdShippingMethodResponseDto.ShippingMethodId },
                            new ResponseDto()
                            {
                                Data = createdShippingMethodResponseDto,
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
        public async Task<IActionResult> UpdateShippingMethod([FromRoute] int? id, [FromBody] ShippingMethodRequestDto shippingMethodRequestDto)
        {
            if (!id.HasValue || shippingMethodRequestDto == null)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "shippingMethodId is required" });
            }

            try
            {
                var updatedShippingMethodResponseDto = await _shippingMethodService.UpdateShippingMethod(id.Value, shippingMethodRequestDto);

                return Ok(new ResponseDto()
                {
                    Data = updatedShippingMethodResponseDto
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
        public async Task<IActionResult> DeleteShippingMethod([FromRoute] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "shippingMethodId is required" });
            }

            try
            {
                var deletedShippingMethodResponseDto = await _shippingMethodService.DeleteShippingMethod(id.Value);

                return Ok(new ResponseDto()
                {
                    Data = deletedShippingMethodResponseDto
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
