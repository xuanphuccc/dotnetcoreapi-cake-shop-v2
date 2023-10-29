using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetcoreapi.cake.shop.domain
{
    public interface IBaseRepository<TEntity> : IBaseReadOnlyRepository<TEntity>
    {
        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Số bản ghi được thêm thành công</returns>
        Task<TEntity> CreateEntityAsync(TEntity entity);

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần cập nhật</param>
        /// <returns>Số bản ghi cập nhật thành công</returns>
        Task<TEntity> UpdateEntityAsync(TEntity entity);

        /// <summary>
        /// Xoá một bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần xoá</param>
        /// <returns>Số bản ghi xoá thành công</returns>
        Task<TEntity> DeleteEntityAsync(TEntity entity);
    }
}
