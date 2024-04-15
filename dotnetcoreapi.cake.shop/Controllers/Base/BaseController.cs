using dotnetcoreapi.cake.shop.application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcoreapi.cake.shop
{
    public abstract class BaseController<TEntityDto, TEntityCreateDto, TEntityUpdateDto> :
        BaseReadOnlyController<TEntityDto>
    {
        #region Fields
        protected readonly IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> _baseService;
        #endregion

        #region Constructors
        protected BaseController(IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> baseService) : base(baseService)
        {
            _baseService = baseService;
        }
        #endregion

        #region Endpoints
        /// <summary>
        /// Tạo mới một đối tượng
        /// </summary>
        /// <param name="entityCreateDto">Data đối tượng cần tạo</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TEntityCreateDto entityCreateDto)
        {
            var result = await _baseService.CreateEntityAsync(entityCreateDto);

            return StatusCode(StatusCodes.Status201Created, new ResponseDto() { Data = result });
        }

        /// <summary>
        /// Sửa đối tượng theo id
        /// </summary>
        /// <param name="id">Mã đối tượng</param>
        /// <param name="entityUpdateDto">Data đối tượng cần sửa</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] TEntityUpdateDto entityUpdateDto)
        {
            var result = await _baseService.UpdateEntityAsync(id, entityUpdateDto);

            return Ok(new ResponseDto() { Data = result });
        }

        /// <summary>
        /// Xoá một đối tượng theo Id
        /// </summary>
        /// <param name="id">Mã đối tượng</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] int id)
        {
            var result = await _baseService.DeleteEntityAsync(id);

            return Ok(new ResponseDto() { Data = result });
        }
        #endregion
    }
}
