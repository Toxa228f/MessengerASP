using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Letter.Domain.Entities;

namespace Letter.Dal.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<User> GetByEmail(string email);
        public Task<User> GetByUserName(string userName);
    }
}
