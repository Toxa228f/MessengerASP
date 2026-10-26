using Letter.Domain.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Service.Interfaces
{
    public interface IBaseService<T>
    {
        public Task<IBaseResponce<IEnumerable<T>>> SelectAllAsync();

        public Task<IBaseResponce<T>> SelectByIdAsync(long id);
        public Task<IBaseResponce<bool>> CreateAsync(T entity);
        public Task<IBaseResponce<bool>> DeleteAsync(T entity);
        public Task<IBaseResponce<bool>> UpdateAsync(T entity);

        void SendErrorMsg(string msg);
    }
 }
