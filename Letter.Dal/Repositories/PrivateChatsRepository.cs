using Letter.Dal.Interfaces;
using Letter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Dal.Repositories
{
    public class PrivateChatsRepository : IPrivateChatsRepository
    {
        private readonly ApplicationDBContext db;

        public PrivateChatsRepository(ApplicationDBContext db) => this.db = db;

        public async Task<bool> Create(PrivateChat entity)
        {
            await db.PrivateChats.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(PrivateChat entity)
        {
            db.PrivateChats.Remove(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(PrivateChat entity)
        {
            db.PrivateChats.Update(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public IQueryable<PrivateChat> Get() => db.PrivateChats;

        public async Task<PrivateChat> GetByCode(string code) => await db.PrivateChats.FirstOrDefaultAsync(x => x.Code == code);

        public async Task<List<PrivateChat>> GetByFirstUserId(long id) => await db.PrivateChats.Where(x => x.UserFirstId == id).ToListAsync();

        public async Task<PrivateChat> GetById(long id) => await db.PrivateChats.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<PrivateChat>> GetBySecondUserId(long id) => await db.PrivateChats.Where(x => x.UserSecondId == id).ToListAsync();

        public async Task<List<PrivateChat>> SelectAll() => await db.PrivateChats.ToListAsync();

        
    }
}
