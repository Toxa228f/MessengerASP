using Letter.Domain.Entities;
using Letter.Domain.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Service.Interfaces
{
    public interface IUserInChatService : IBaseService<UserInChat>
    {
        public Task<IBaseResponce<UserInChat>> GetFirstUserInChatByUserIdAsync(long id);
        public Task<IBaseResponce<UserInChat>> GetFirstUserInChatByChatIdAsync(long id);

        public Task<IBaseResponce<IEnumerable<UserInChat>>> GetAllUserInChatByUserIdAsync(long id);
        public Task<IBaseResponce<IEnumerable<UserInChat>>> GetAllUserInChatByChatIdAsync(long id);

        public Task<IBaseResponce<bool>> IsUserInChatWithUserIdAndChatId(long userId,long chatId);

        public Task<IBaseResponce<bool>> CreateUserInChatWithNewChat(long userId, Chat chat);
    }
}
