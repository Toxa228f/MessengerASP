using Letter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Dal.Interfaces
{
    public interface IPrivateChatsRepository : IBaseRepository<PrivateChat>
    {
        public Task<PrivateChat> GetByCode(string code);
        public Task<List<PrivateChat>> GetByFirstUserId(long id);
        public Task<List<PrivateChat>> GetBySecondUserId(long id);
    }
}
