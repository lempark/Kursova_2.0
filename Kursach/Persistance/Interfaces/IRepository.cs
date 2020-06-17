using System.Collections.Generic;
using System.Linq;

namespace Persistance.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        IQueryable<TEntity> GetAllQueryable();
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
