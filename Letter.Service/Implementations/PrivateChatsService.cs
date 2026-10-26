using Letter.Domain.Entities;
using Letter.Domain.Responces;
using Letter.Domain.Enums;
using Letter.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Letter.Dal.Interfaces;
using Letter.Domain.Helpers;

namespace Letter.Service.Implementations
{
    public class PrivateChatsService : IPrivateChatsService
    {
        public readonly IPrivateChatsRepository rep;

        public PrivateChatsService(IPrivateChatsRepository rep) => this.rep=rep;

        public async Task<IBaseResponce<bool>> CreateAsync(PrivateChat entity)
        {
            BaseResponce<bool> responce = new();
            try
            {
                bool isCreate = await rep.Create(entity);
                if (isCreate)
                {
                    responce.Code = ResponceCodes.Ok;
                    responce.Data = true;
                    responce.Responce = entity.Code;
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

        public async Task<IBaseResponce<bool>> DeleteAsync(PrivateChat entity)
        {
            BaseResponce<bool> responce = new();
            try
            {
                bool isCreate = await rep.Delete(entity);
                if (isCreate)
                {
                    responce.Code = ResponceCodes.Ok;
                    responce.Data = true;
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

        public async Task<IBaseResponce<bool>> UpdateAsync(PrivateChat entity)
        {
            BaseResponce<bool> responce = new();
            try
            {
                bool isCreate = await rep.Update(entity);
                if (isCreate)
                {
                    responce.Code = ResponceCodes.Ok;
                    responce.Data = true;
                    responce.Responce = entity.Code;
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

        public async Task<IBaseResponce<PrivateChat>> GetByCode(string code)
        {
            var responce = new BaseResponce<PrivateChat>();
            try
            {
                var pchat = await rep.GetByCode(code);
                if (pchat!=null)
                {
                    responce.Code = ResponceCodes.Ok;
                    responce.Data = pchat;
                }
                return responce;
            }
            catch (Exception)
            {
                return new BaseResponce<PrivateChat>()
                {
                    Code = ResponceCodes.UnCatchedError,
                    Data = null,
                    Responce = "Необработанная ошибка"
                };
            }
        }

        public async Task<IBaseResponce<IEnumerable<PrivateChat>>> GetByFirstUserId(long id)
        {
            var responce = new BaseResponce<IEnumerable<PrivateChat>>();
            try
            {
                var pchats = await rep.GetByFirstUserId(id);
                if (pchats.Count <=0 || pchats == null)
                {
                    responce.Code = ResponceCodes.NullTable;
                    responce.Data = null;
                    responce.Responce = "Пустая таблица";
                }
                else
                {
                    responce.Code = ResponceCodes.Ok;
                    responce.Data = pchats;
                }
                return responce;
            }
            catch (Exception)
            {
                return new BaseResponce<IEnumerable<PrivateChat>>()
                {
                    Code = ResponceCodes.UnCatchedError,
                    Data = null,
                    Responce = "Необработанная ошибка"
                };
            }
        }

        public async Task<IBaseResponce<IEnumerable<PrivateChat>>> GetBySecondUserId(long id)
        {
            var responce = new BaseResponce<IEnumerable<PrivateChat>>();
            try
            {
                var pchats = await rep.GetBySecondUserId(id);
                if (pchats.Count <= 0 || pchats == null)
                {
                    responce.Code = ResponceCodes.NullTable;
                    responce.Data = null;
                    responce.Responce = "Пустая таблица";
                }
                else
                {
                    responce.Code = ResponceCodes.Ok;
                    responce.Data = pchats;
                }
                return responce;
            }
            catch (Exception)
            {
                return new BaseResponce<IEnumerable<PrivateChat>>()
                {
                    Code = ResponceCodes.UnCatchedError,
                    Data = null,
                    Responce = "Необработанная ошибка"
                };
            }
        }

        public async Task<IBaseResponce<IEnumerable<PrivateChat>>> SelectAllAsync()
        {
            var responce = new BaseResponce<IEnumerable<PrivateChat>>();
            try
            {
                var pchats = await rep.SelectAll();
                if (pchats.Count <= 0 || pchats == null)
                {
                    responce.Code = ResponceCodes.NullTable;
                    responce.Data = null;
                    responce.Responce = "Пустая таблица";
                }
                else
                {
                    responce.Code = ResponceCodes.Ok;
                    responce.Data = pchats;
                }
                return responce;
            }
            catch (Exception)
            {
                return new BaseResponce<IEnumerable<PrivateChat>>()
                {
                    Code = ResponceCodes.UnCatchedError,
                    Data = null,
                    Responce = "Необработанная ошибка"
                };
            }
        }

        public async Task<IBaseResponce<PrivateChat>> SelectByIdAsync(long id)
        {
            var responce = new BaseResponce<PrivateChat>();
            try
            {
                var pchat = await rep.GetById(id);
                if (pchat == null)
                {
                    responce.Code = ResponceCodes.NullTable;
                    responce.Data = null;
                    responce.Responce = "Пустая таблица";
                }
                else
                {
                    responce.Code = ResponceCodes.Ok;
                    responce.Data = pchat;
                }
                return responce;
            }
            catch (Exception)
            {
                return new BaseResponce<PrivateChat>()
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

        public async Task<IBaseResponce<bool>> CreateNewAsync(PrivateChat entity)
        {
            BaseResponce<bool> responce = new();
            try
            {
                Random rnd = new Random();
                string code = Convert.ToString(rnd.Next(100000, 10000000), 16).ToUpper();
                entity.Code = HelperHashCode.HashString(code);

                bool isCreate = await rep.Create(entity);
                if (isCreate)
                {
                    responce.Code = ResponceCodes.Ok;
                    responce.Data = true;
                    responce.Responce = entity.Code;
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

        public async Task<IBaseResponce<bool>> AddNewMessage(long id, string text)
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
