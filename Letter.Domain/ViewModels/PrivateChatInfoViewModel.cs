using Letter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Domain.ViewModels
{
    public class PrivateChatInfoViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string MyUserName { get; set; }
        public long MessagesCount { get; set; }
        public PrivateChatRole Role { get; set; }
    }
}
