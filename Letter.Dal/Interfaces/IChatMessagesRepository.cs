using Letter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Dal.Interfaces
{
    public interface IChatMessagesRepository:IBaseRepository<ChatMessage>
    {
        public IQueryable<ChatMessage> SelectAllByChatId(long chatId);
    }
}
