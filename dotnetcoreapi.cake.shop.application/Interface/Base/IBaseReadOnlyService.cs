using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetcoreapi.cake.shop.application
{
    public interface IBaseReadOnlyService<TEntityDto>
    {
        /// <summary>
        /// Lấy toàn bộ danh sách
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntityDto>> GetAllEntitiesAsync();

        /// <summary>
        /// Lấy một bản ghi theo ID
        /// </summary>
        /// <param name="entityId">ID của bản ghi</param>
        /// <returns></returns>
        Task<TEntityDto> GetEntityByIdAsync(int entityId);
    }
}
