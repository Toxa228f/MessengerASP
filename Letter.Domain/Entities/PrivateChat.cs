using Letter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Domain.Entities
{
    public class PrivateChat
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long UserFirstId { get; set; }
        public long UserSecondId { get; set; }
        public long MessagesCount { get; set; }
        public string LastMessage { get; set; }
        public PrivateChatRole Role { get; set; }
    }
}
