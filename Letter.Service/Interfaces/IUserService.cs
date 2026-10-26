using Letter.Dal.Interfaces;
using Letter.Domain.Entities;
using Letter.Domain.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Service.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        public Task<IBaseResponce<User>> GetByEmailAsync(string email);
        public Task<IBaseResponce<User>> GetByUserNameAsync(string userName);

        public Task<IBaseResponce<ClaimsIdentity>> CreateNewUserAsync(User user);
        public Task<IBaseResponce<ClaimsIdentity>> LoginUser(User user);


        public ClaimsIdentity Authentication(User user);
        
    }
}
