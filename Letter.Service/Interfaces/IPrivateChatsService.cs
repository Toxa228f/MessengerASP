using Letter.Domain.Entities;
using Letter.Domain.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Service.Interfaces
{
    public interface IPrivateChatsService : IBaseService<PrivateChat>
    {
        public Task<IBaseResponce<PrivateChat>> GetByCode(string code);
        public Task<IBaseResponce<IEnumerable<PrivateChat>>> GetByFirstUserId(long id);
        public Task<IBaseResponce<IEnumerable<PrivateChat>>> GetBySecondUserId(long id);

        public Task<IBaseResponce<bool>> CreateNewAsync(PrivateChat entity);


        public Task<IBaseResponce<bool>> AddNewMessage(long id, string text);
    }
}
