using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetcoreapi.cake.shop.domain
{
    public interface IBaseReadOnlyRepository<TEntity>
    {
        IQueryable<TEntity> GetAllCategories();
    }
}
