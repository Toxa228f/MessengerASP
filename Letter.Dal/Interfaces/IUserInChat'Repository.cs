using Letter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Dal.Interfaces
{
    public interface IUserInChatRepository : IBaseRepository<UserInChat>
    {
        public Task<UserInChat> GetFirstByUserId(long id);
        public Task<UserInChat> GetFirstByChatId(long id);
        public Task<UserInChat> GetFirstByUserIdAndChatId(long userId, long chatId);
    }
}
