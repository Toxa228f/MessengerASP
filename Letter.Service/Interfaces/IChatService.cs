using Letter.Domain.Entities;
using Letter.Domain.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Service.Interfaces
{
    public interface IChatService : IBaseService<Chat>
    {
        public Task<IBaseResponce<Chat>> GetByChatCodeAsync(string code);

        public Task<IBaseResponce<Chat>> GetByChatCodeHashAsync(string code);
        
        public Task<IBaseResponce<bool>> CreateNewChatAsync(Chat chat);

        public IBaseResponce<Chat> GetById(long id);

        public Task<IBaseResponce<IEnumerable<Chat>>> GetChatByUserInChatByUserIdAsync(long id);

        public Task<IBaseResponce<bool>> AddNewUser(long id);
        public Task<IBaseResponce<bool>> AddNewMessage(long id,string text);
    }
}
