using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IService<T>
    {
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<T> Get(Guid Id);
        Task<List<T>> GetAll();
        Task<Tuple<long, List<T>>> GetAll(int currentPage, int pageSize, T entity);
    }
}
