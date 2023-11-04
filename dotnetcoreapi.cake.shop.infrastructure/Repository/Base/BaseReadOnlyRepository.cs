using dotnetcoreapi.cake.shop.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetcoreapi.cake.shop.infrastructure
{
    public abstract class BaseReadOnlyRepository<TEntity> : IBaseReadOnlyRepository<TEntity> where TEntity : class
    {
        #region Fields
        protected readonly CakeShopContext _context;
        #endregion

        #region Constructors
        public BaseReadOnlyRepository(CakeShopContext context)
        {
            _context = context;
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Lấy toàn bộ danh sách
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetAllEntities()
        {
            var entities = _context.Set<TEntity>().AsQueryable();

            return entities;
        }

        /// <summary>
        /// Lấy một bản ghi theo ID
        /// </summary>
        /// <param name="entityId">ID của bản ghi</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetEntityByIdAsync(int entityId)
        {
            var entity = await _context.Set<TEntity>().FindAsync(entityId);

            if (entity == null)
            {
                throw new NotFoundException("Tài nguyên không tồn tại", ErrorCode.NotFound);
            }

            return entity;
        }
        #endregion
    }
}
