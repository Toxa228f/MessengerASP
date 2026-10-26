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
    public class PrivateChatMessagesService : IPrivateChatMessagesService
    {
        private readonly IPrivateChatMessagesRepository rep;
        public PrivateChatMessagesService(IPrivateChatMessagesRepository rep) => this.rep = rep;

        public async Task<IBaseResponce<bool>> CreateAsync(PrivateChatMessage entity)
        {
            var responce = new BaseResponce<bool>()
            {
                Code = ResponceCodes.BoolError,
                Data = false,
            };
            try
            {
                var ans = await rep.Create(entity);
                if (ans)
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

        public Task<IBaseResponce<bool>> DeleteAsync(PrivateChatMessage entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponce<IEnumerable<PrivateChatMessage>>> GetAllByChatId(long id)
        {
            var responce = new BaseResponce<IEnumerable<PrivateChatMessage>>()
            {
                Code = ResponceCodes.NullTable,
                Data = null,
            };
            try
            {
                var data = rep.SelectAllByChatId(id);
                if (data != null)
                {
                    responce.Data = data;
                    responce.Code = ResponceCodes.Ok;
                }
                return responce;
            }
            catch (Exception)
            {
                return new BaseResponce<IEnumerable<PrivateChatMessage>>()
                {
                    Code = ResponceCodes.UnCatchedError,
                    Data = null,
                    Responce = "Необработанная ошибка"
                };
            }
        }

        public Task<IBaseResponce<IEnumerable<PrivateChatMessage>>> SelectAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponce<PrivateChatMessage>> SelectByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public void SendErrorMsg(string msg)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponce<bool>> UpdateAsync(PrivateChatMessage entity)
        {
            throw new NotImplementedException();
        }
    }
}
