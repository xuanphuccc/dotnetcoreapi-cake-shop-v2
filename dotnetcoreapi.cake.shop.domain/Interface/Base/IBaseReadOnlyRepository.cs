using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetcoreapi.cake.shop.domain
{
    public interface IBaseReadOnlyRepository<TEntity>
    {
        /// <summary>
        /// Lấy toàn bộ danh sách
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAllEntities();

        /// <summary>
        /// Lấy một bản ghi theo ID
        /// </summary>
        /// <param name="entityId">ID của bản ghi</param>
        /// <returns></returns>
        Task<TEntity> GetEntityByIdAsync(int entityId);
    }
}
