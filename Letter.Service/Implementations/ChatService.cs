using Letter.Domain.Entities;
using Letter.Domain.Responces;
using Letter.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Letter.Domain.Enums;
using Letter.Dal.Interfaces;
using Letter.Domain.Helpers;
using System.Xml.Linq;

namespace Letter.Service.Implementations
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository rep;
        private readonly IUserInChatRepository rep2;

        public ChatService(IChatRepository rep, IUserInChatRepository rep2)
        {
            this.rep = rep;
            this.rep2 = rep2;
        }

        public async Task<IBaseResponce<bool>> CreateAsync(Chat entity)
        {
            var responce = new BaseResponce<bool>();
            try
            {
                bool data = await rep.Create(entity);
                if (!data)
                {
                    responce.Code = ResponceCodes.BoolError;
                }
                else { responce.Code = ResponceCodes.Ok; }
                responce.Data = data;
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

        public async Task<IBaseResponce<bool>> DeleteAsync(Chat entity)
        {
            var responce = new BaseResponce<bool>();
            try
            {
                bool data = await rep.Delete(entity);
                if (!data)
                {
                    responce.Code = ResponceCodes.BoolError;
                }
                else { responce.Code = ResponceCodes.Ok; }
                responce.Data = data;
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

        public async Task<IBaseResponce<Chat>> GetByChatCodeAsync(string code)
        {
            var responce = new BaseResponce<Chat>();
            try
            {
                var data = await rep.GetByChatCode(HelperHashCode.HashString(code));
                if (data == null)
                {
                    responce.Code = ResponceCodes.NullTable;
                }
                else { responce.Code = ResponceCodes.Ok; }
                responce.Data = data;
                return responce;
            }
            catch (Exception)
            {
                return new BaseResponce<Chat>()
                {
                    Code = ResponceCodes.UnCatchedError,
                    Data = null,
                    Responce = "Необработанная ошибка"
                };
            }
        }

        public async Task<IBaseResponce<IEnumerable<Chat>>> SelectAllAsync()
        {
            var responce = new BaseResponce<IEnumerable<Chat>>();
            try
            {
                var data = await rep.SelectAll();
                if (data == null)
                {
                    responce.Code = ResponceCodes.NullTable;
                }
                else { responce.Code = ResponceCodes.Ok; }
                responce.Data = data;
                return responce;
            }
            catch (Exception)
            {
                return new BaseResponce<IEnumerable<Chat>>()
                {
                    Code = ResponceCodes.UnCatchedError,
                    Data = null,
                    Responce = "Необработанная ошибка"
                };
            }
        }

        public async Task<IBaseResponce<Chat>> SelectByIdAsync(long id)
        {
            var responce = new BaseResponce<Chat>();
            try
            {
                var data = await rep.GetById(id);
                if (data == null)
                {
                    responce.Code = ResponceCodes.NullTable;
                }
                else { responce.Code = ResponceCodes.Ok; }
                responce.Data = data;
                return responce;
            }
            catch (Exception)
            {
                return new BaseResponce<Chat>()
                {
                    Code = ResponceCodes.UnCatchedError,
                    Data = null,
                    Responce = "Необработанная ошибка"
                };
            }
        }

        public void SendErrorMsg(string msg)
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponce<bool>> UpdateAsync(Chat entity)
        {
            var responce = new BaseResponce<bool>();
            try
            {
                bool data = await rep.Update(entity);
                if (!data)
                {
                    responce.Code = ResponceCodes.BoolError;
                }
                else { responce.Code = ResponceCodes.Ok; }
                responce.Data = data;
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



        public async Task<IBaseResponce<bool>> CreateNewChatAsync(Chat chat)
        {
            var responce = new BaseResponce<bool>();
            try
            {
                Random rnd = new Random();
                string code = Convert.ToString(rnd.Next(100000, 10000000), 16).ToUpper()+ Convert.ToString(rnd.Next(100000, 10000000), 16).ToUpper();
                chat.ChatCode = HelperHashCode.HashString(code);
                var data = await CreateAsync(chat);
                if (!data.Data)
                {
                    responce.Code = ResponceCodes.BoolError;
                }
                else { responce.Code = ResponceCodes.Ok; }
                responce.Data = data.Data;
                responce.Responce = code;
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

        public IBaseResponce<Chat> GetById(long id)
        {
            var responce = new BaseResponce<Chat>();
            try
            {
                var data = rep.Get().FirstOrDefault(x => x.Id == id);
                responce.Code = ResponceCodes.Ok;
                if (data == null)
                {
                    responce.Code = ResponceCodes.NullTable;
                }

                responce.Data = data;

                return responce;
            }
            catch (Exception)
            {
                return new BaseResponce<Chat>()
                {
                    Code = ResponceCodes.UnCatchedError,
                    Data = null,
                    Responce = "Необработанная ошибка"
                };
            }
        }

        public async Task<IBaseResponce<IEnumerable<Chat>>> GetChatByUserInChatByUserIdAsync(long id)
        {
            try
            {
                var result = new BaseResponce<IEnumerable<Chat>>();
                result.Data = null;

                var data = rep2.Get().Where(x => x.UserId == id);
                if (data == null)
                {
                    return result;
                }

                var chats = from c in rep.Get()
                            join v in rep2.Get().Where(x=> x.UserId==id) on c.Id equals v.ChatId 
                            select c;


                if (chats==null)
                {
                    result.Code = ResponceCodes.NullTable;
                    return result;
                }
                
                result.Code = ResponceCodes.Ok;
                result.Data = chats;

                return result;
            }
            catch (Exception)
            {
                return new BaseResponce<IEnumerable<Chat>>()
                {
                    Code = ResponceCodes.UnCatchedError,
                    Data = null,
                    Responce = "Необработанная ошибка"
                };
            }
        }

        public async Task<IBaseResponce<bool>> AddNewUser(long id)
        {
            var responce = new BaseResponce<bool>();
            try
            {
                var chat = await SelectByIdAsync(id);
                if (chat.Data == null)
                {
                    responce.Code = ResponceCodes.NullTable;
                    responce.Data = false;
                    return responce;
                }
                chat.Data.UserCount++;
                bool x = await rep.Update(chat.Data);

                responce.Code = ResponceCodes.Ok;
                responce.Data = x;

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

        public async Task<IBaseResponce<Chat>> GetByChatCodeHashAsync(string code)
        {
            var responce = new BaseResponce<Chat>();
            try
            {
                var data = await rep.GetByChatCode(code);
                if (data == null)
                {
                    responce.Code = ResponceCodes.NullTable;
                }
                else { responce.Code = ResponceCodes.Ok; }
                responce.Data = data;
                return responce;
            }
            catch (Exception)
            {
                return new BaseResponce<Chat>()
                {
                    Code = ResponceCodes.UnCatchedError,
                    Data = null,
                    Responce = "Необработанная ошибка"
                };
            }
        }

        public async Task<IBaseResponce<bool>> AddNewMessage(long id,string text)
        {
            var responce = new BaseResponce<bool>();
            try
            {
                var chat = await SelectByIdAsync(id);
                if (chat.Data == null)
                {
                    responce.Code = ResponceCodes.NullTable;
                    responce.Data = false;
                    return responce;
                }
                chat.Data.MessagesCount++;
                chat.Data.LastMessage = text;
                bool x = await rep.Update(chat.Data);

                responce.Code = ResponceCodes.Ok;
                responce.Data = x;

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
