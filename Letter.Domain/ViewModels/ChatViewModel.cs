using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Domain.ViewModels
{
    public class ChatViewModel
    {
        public string Name { get; set; }
        public string ChatCode { get; set; }
        public string Description { get; set; }
        //public Bitmap Image { get; set; }
        public int UserCount { get; set; }
        public long MessagesCount { get; set; }
        public string LastMessage { get;set;}
    }
}
