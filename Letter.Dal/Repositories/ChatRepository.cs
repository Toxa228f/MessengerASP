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
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDBContext db;
        public ChatRepository(ApplicationDBContext db) => this.db = db;

        public async Task<bool> Create(Chat entity)
        {
            await db.Chats.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Chat entity)
        {
            db.Chats.Remove(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<Chat> GetById(long id) => await db.Chats.FirstOrDefaultAsync(x=>x.Id==id);

        public IQueryable<Chat> Get() => db.Chats;

        public async Task<Chat> GetByChatCode(string code) => await db.Chats.FirstOrDefaultAsync(x => x.ChatCode == code);

        public async Task<List<Chat>> SelectAll() => await db.Chats.ToListAsync(); 

        public async Task<bool> Update(Chat entity)
        {
            db.Chats.Update(entity);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
