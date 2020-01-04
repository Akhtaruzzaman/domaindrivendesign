using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepo
{
    public interface IRepository<T> : IDisposable
        where T : Entity
    {
        Task<bool> Add(T entity);
        Task<bool> AddRange(List<T> entities);
        Task<bool> Update(T entity);
        Task<bool> UpdateRange(List<T> entities);
        Task<bool> Delete(Guid id);
        Task<bool> DeleteRange(List<T> entities);
        Task<T> Get(Guid id);
        Task<IQueryable<T>> GetAll(Guid id);
        Task<IQueryable<T>> GetAll(Expression<Func<T, bool>> expression);
        Task<T> GetAny(Expression<Func<T, bool>> expression);
        Guid GetGuid();
    }
}
