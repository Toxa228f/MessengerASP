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
    public class ChatMessagesRepository : IChatMessagesRepository
    {
        private readonly ApplicationDBContext db;
        public ChatMessagesRepository(ApplicationDBContext db) => this.db = db;

        public async Task<bool> Create(ChatMessage entity)
        {
            await db.ChatMessages.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(ChatMessage entity)
        {
            db.ChatMessages.Remove(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public IQueryable<ChatMessage> Get()
        {
            return db.ChatMessages;
        }

        public async Task<ChatMessage> GetById(long id) => await db.ChatMessages.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<ChatMessage>> SelectAll()=> await db.ChatMessages.ToListAsync();

        public IQueryable<ChatMessage> SelectAllByChatId(long chatId)=> db.ChatMessages.Where(x=>x.ChatId==chatId);

        public async Task<bool> Update(ChatMessage entity)
        {
            db.ChatMessages.Update(entity);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
