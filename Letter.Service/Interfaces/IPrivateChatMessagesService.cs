using Letter.Domain.Entities;
using Letter.Domain.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Service.Interfaces
{
    public interface IPrivateChatMessagesService : IBaseService<PrivateChatMessage>
    {
        public Task<IBaseResponce<IEnumerable<PrivateChatMessage>>> GetAllByChatId(long id);
    }
}
