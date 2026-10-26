using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Domain.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public DateTime BornDate { get; set; }
        public string Password { get; set; }
    }
}
