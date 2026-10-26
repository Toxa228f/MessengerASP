using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Domain.Entities
{
    public class PrivateChatMessage
    {
        public long Id { get; set; }
        public long PrivateChatId { get; set; }
        public string UserName { get; set; }
        public string MessageText { get; set; }
        public string DataTime { get; set; }
    }
}
