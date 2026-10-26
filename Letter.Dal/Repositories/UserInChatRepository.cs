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
    public class UserInChatRepository : IUserInChatRepository
    {
        private readonly ApplicationDBContext db;
        public UserInChatRepository(ApplicationDBContext db) => this.db = db;

        public async Task<bool> Create(UserInChat entity)
        {
            await db.UserInChat.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(UserInChat entity)
        {
            db.UserInChat.Remove(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public IQueryable<UserInChat> Get() => db.UserInChat;

        public async Task<UserInChat> GetFirstByChatId(long id) => await db.UserInChat.FirstOrDefaultAsync(x => x.ChatId == id);

        public async Task<UserInChat> GetById(long id) => await db.UserInChat.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<UserInChat> GetFirstByUserId(long id) => await db.UserInChat.FirstOrDefaultAsync(x => x.UserId == id);

        public async Task<List<UserInChat>> SelectAll() => await db.UserInChat.ToListAsync();

        public async Task<bool> Update(UserInChat entity)
        {
            db.UserInChat.Update(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<UserInChat> GetFirstByUserIdAndChatId(long userId, long chatId) => await db.UserInChat.FirstOrDefaultAsync(x => x.UserId == userId && x.ChatId == chatId);
    }
}
