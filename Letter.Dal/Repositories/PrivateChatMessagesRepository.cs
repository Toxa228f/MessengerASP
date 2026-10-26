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
    public class PrivateChatMessagesRepository : IPrivateChatMessagesRepository
    {
        private readonly ApplicationDBContext db;
        public PrivateChatMessagesRepository(ApplicationDBContext db) => this.db = db;

        public async Task<bool> Create(PrivateChatMessage entity)
        {
            await db.PrivateChatMessages.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(PrivateChatMessage entity)
        {
            db.PrivateChatMessages.Remove(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public IQueryable<PrivateChatMessage> Get()
        {
            return db.PrivateChatMessages;
        }

        public async Task<PrivateChatMessage> GetById(long id) => await db.PrivateChatMessages.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<PrivateChatMessage>> SelectAll() => await db.PrivateChatMessages.ToListAsync();

        public IQueryable<PrivateChatMessage> SelectAllByChatId(long chatId) => db.PrivateChatMessages.Where(x => x.PrivateChatId == chatId);

        public async Task<bool> Update(PrivateChatMessage entity)
        {
            db.PrivateChatMessages.Update(entity);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
