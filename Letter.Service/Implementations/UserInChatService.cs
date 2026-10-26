using Letter.Dal.Interfaces;
using Letter.Domain.Entities;
using Letter.Domain.Enums;
using Letter.Domain.Responces;
using Letter.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Service.Implementations
{
    public class UserInChatService : IUserInChatService
    {
        private readonly IUserInChatRepository rep;
        private readonly IUserService serU;
        private readonly IChatService serC;
        public UserInChatService(IUserInChatRepository rep, IUserService serU, IChatService serC)
        {
            this.rep = rep;
            this.serU = serU;
            this.serC = serC;
        }

        public async Task<IBaseResponce<bool>> CreateAsync(UserInChat entity)
        {
            var responce = new BaseResponce<bool>();
            try
            {
                UserInChat check = await rep.GetFirstByUserIdAndChatId(entity.UserId,entity.ChatId);
                
                if (check!=null)
                {
                    responce.Code = ResponceCodes.UserExist;
                    responce.Data = false;
                    return responce;
                }
                var data = await rep.Create(entity);
                var data2 = await serC.AddNewUser(entity.ChatId);

                responce.Code = ResponceCodes.Ok;
                responce.Data = data && data2.Data;
                if (!data)
                {
                    responce.Code = ResponceCodes.BoolError;
                }
                return responce;
            }
            catch (Exception)
            {
                return new BaseResponce<bool>()
                {
                    Code = ResponceCodes.UnCatchedError,
                    Data = false,
                    Responce = "Необработанная ошибка"
                };
            }
        }

        public async Task<IBaseResponce<bool>> DeleteAsync(UserInChat entity)
        {
            var responce = new BaseResponce<bool>();
            try
            {
                var data = await rep.Delete(entity);
                responce.Code = ResponceCodes.Ok;
                responce.Data = data;
                if (!data)
                {
                    responce.Code = ResponceCodes.BoolError;
                }
                return responce;
            }
            catch (Exception)
            {
                return new BaseResponce<bool>()
                {
                    Code = ResponceCodes.UnCatchedError,
                    Data = false,
                    Responce = "Необработанная ошибка"
                };
            }
        }

        public async Task<IBaseResponce<IEnumerable<UserInChat>>> GetAllUserInChatByChatIdAsync(long id)
        {
            var responce = new BaseResponce<IEnumerable<UserInChat>>();
            try
            {
                var data = rep.Get().Where(x => x.ChatId == id);

                responce.Code = ResponceCodes.Ok;
                responce.Data = data;
                if (data == null)
                {
                    responce.Code = ResponceCodes.NullTable;
                }
                return responce;
            }
            catch (Exception)
            {
                return new BaseResponce<IEnumerable<UserInChat>>()
                {
                    Code = ResponceCodes.UnCatchedError,
                    Data = null,
                    Responce = "Необработанная ошибка"
                };
            }
        }

        public async Task<IBaseResponce<IEnumerable<UserInChat>>> GetAllUserInChatByUserIdAsync(long id)
        {
            var responce = new BaseResponce<IEnumerable<UserInChat>>();
            try
            {
                var data = rep.Get().Where(x => x.UserId == id);

                responce.Code = ResponceCodes.Ok;
                responce.Data = data;
                if (data == null)
                {
                    responce.Code = ResponceCodes.NullTable;
                }
                return responce;
            }
            catch (Exception)
            {
                return new BaseResponce<IEnumerable<UserInChat>>()
                {
                    Code = ResponceCodes.UnCatchedError,
                    Data = null,
                    Responce = "Необработанная ошибка"
                };
            }
        }

        public async Task<IBaseResponce<UserInChat>> GetFirstUserInChatByChatIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponce<UserInChat>> GetFirstUserInChatByUserIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponce<IEnumerable<UserInChat>>> SelectAllAsync()
        {
            var responce = new BaseResponce<IEnumerable<UserInChat>>();
            try
            {
                var data = await rep.SelectAll();
                responce.Code = ResponceCodes.Ok;
                responce.Data = data;
                if (data == null)
                {
                    responce.Code = ResponceCodes.NullTable;
                }
                return responce;
            }
            catch (Exception)
            {
                return new BaseResponce<IEnumerable<UserInChat>>()
                {
                    Code = ResponceCodes.UnCatchedError,
                    Data = null,
                    Responce = "Необработанная ошибка"
                };
            }
        }

        public async Task<IBaseResponce<UserInChat>> SelectByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public void SendErrorMsg(string msg)
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponce<bool>> UpdateAsync(UserInChat entity)
        {
            var responce = new BaseResponce<bool>();
            try
            {
                var data = await rep.Update(entity);
                responce.Code = ResponceCodes.Ok;
                responce.Data = data;
                if (!data)
                {
                    responce.Code = ResponceCodes.BoolError;
                }
                return responce;
            }
            catch (Exception)
            {
                return new BaseResponce<bool>()
                {
                    Code = ResponceCodes.UnCatchedError,
                    Data = false,
                    Responce = "Необработанная ошибка"
                };
            }
        }

        public async Task<IBaseResponce<bool>> CreateUserInChatWithNewChat(long userId, Chat chat)
        {
            var responce = new BaseResponce<bool>();
            try
            {
                var firstResponce = await serC.CreateNewChatAsync(chat);
                var newchat = await serC.GetByChatCodeAsync(firstResponce.Responce);


                UserInChat userInChat = new UserInChat()
                {
                    UserId = userId,
                    ChatId = newchat.Data.Id,
                };

                var data = await CreateAsync(userInChat);
                var data2 = await serC.AddNewUser(newchat.Data.Id);

                responce.Data = data.Data && data2.Data;
                responce.Code = data.Code;
                responce.Responce = firstResponce.Responce;

                return responce;
            }
            catch (Exception)
            {
                return new BaseResponce<bool>()
                {
                    Code = ResponceCodes.UnCatchedError,
                    Data = false,
                    Responce = "Необработанная ошибка"
                };
            }
        }

        public async Task<IBaseResponce<bool>> IsUserInChatWithUserIdAndChatId(long userId, long chatId)
        {
            var responce = new BaseResponce<bool>() { Data = true };
            try
            {
                var data = rep.Get().Where(x => x.ChatId == chatId).Where(x => x.UserId == userId);

                if (data == null) { responce.Data = false; responce.Code = ResponceCodes.NullTable; }
                else responce.Code = ResponceCodes.Ok;
                return responce;
            }
            catch (Exception)
            {
                return new BaseResponce<bool>()
                {
                    Code = ResponceCodes.UnCatchedError,
                    Data = false,
                    Responce = "Необработанная ошибка"
                };
            }
        }
    }
}
