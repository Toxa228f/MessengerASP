using Letter.Domain.Entities;
using Letter.Domain.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Service.Interfaces
{
    public interface IChatMessagesService:IBaseService<ChatMessage>
    {
        public Task<IBaseResponce<IEnumerable<ChatMessage>>> GetAllByChatId(long id);
    }
}
