using dotnetcoreapi.cake.shop.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetcoreapi.cake.shop.application
{
    public interface IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> : IBaseReadOnlyService<TEntityDto>
    {
        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Đối tượng cần thêm</param>
        /// <returns></returns>
        Task<TEntityDto> CreateEntityAsync(TEntityCreateDto entityCreateDto);

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="entityId">ID của đối tượng cần cập nhật</param>
        /// <param name="entityUpdateDto">Đối tượng cần cập nhật</param>
        /// <returns></returns>
        Task<TEntityDto> UpdateEntityAsync(int entityId, TEntityUpdateDto entityUpdateDto);

        /// <summary>
        /// Xoá một bản ghi
        /// </summary>
        /// <param name="entityId">ID của bản ghi cần xoá</param>
        /// <returns></returns>
        Task<TEntityDto> DeleteEntityAsync(int entityId);
    }
}
