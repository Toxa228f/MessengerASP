using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Domain.Entities
{
    public class UserInChat
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ChatId { get; set; }
    }
}
