using dotnetcoreapi.cake.shop.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetcoreapi.cake.shop.infrastructure.Repository.Base
{
    public class BaseReadOnlyRepository<TEntity> : IBaseReadOnlyRepository<TEntity> where TEntity : class
    {
        private readonly CakeShopContext _context;
        public BaseReadOnlyRepository(CakeShopContext context)
        {
            _context = context;
        }

        public virtual IQueryable<TEntity> GetAllCategories()
        {
            var entities = _context.Set<TEntity>().AsQueryable();

            return entities;
        }
    }
}
