using Letter.Dal.Interfaces;
using Letter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Dal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDBContext dbContext;
        public UserRepository(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> Create(User entity)
        {
            await dbContext.Users.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(User entity)
        {
            dbContext.Users.Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetById(long id) => await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

        public IQueryable<User> Get() => dbContext.Users;

        public async Task<User> GetByEmail(string email)
        {
            return await dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetByUserName(string userName) => await dbContext.Users.FirstOrDefaultAsync(x => x.Login == userName);

        public async Task<List<User>> SelectAll() => await dbContext.Users.ToListAsync();

        public async Task<bool> Update(User entity)
        {
            dbContext.Users.Update(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

    }
}
