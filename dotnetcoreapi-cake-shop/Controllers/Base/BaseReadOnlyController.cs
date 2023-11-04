using dotnetcoreapi.cake.shop.application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcoreapi.cake.shop
{
    public abstract class BaseReadOnlyController<TEntityDto> : ControllerBase
    {
        #region Fields
        protected readonly IBaseReadOnlyService<TEntityDto> _baseReadOnlyService;
        #endregion

        #region Constructors
        protected BaseReadOnlyController(IBaseReadOnlyService<TEntityDto> baseReadOnlyService)
        {
            _baseReadOnlyService = baseReadOnlyService;
        }
        #endregion

        #region Endpoints
        /// <summary>
        /// Get toàn bộ danh sách đối tượng
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        /// CreatedBy: txphuc (18/07/2023)
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var entityDtos = await _baseReadOnlyService.GetAllEntitiesAsync();

            return Ok(entityDtos);
        }

        /// <summary>
        /// Get một đối tượng thông qua Id
        /// </summary>
        /// <param name="id">Mã đối tượng</param>
        /// <returns>Trả về thông tin một đối tượng tìm được</returns> 
        /// CreatedBy: txphuc (18/07/2023)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var entityDto = await _baseReadOnlyService.GetEntityByIdAsync(id);

            return Ok(entityDto);
        }
        #endregion
    }
}
