using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Dal.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T entity);

        Task<T> GetById(long id);

        Task<bool> Delete(T entity);

        Task<List<T>> SelectAll();

        IQueryable<T> Get();

        Task<bool> Update(T entity);
    }
}
