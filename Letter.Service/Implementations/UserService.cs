using Letter.Dal.Interfaces;
using Letter.Domain.Entities;
using Letter.Domain.Responces;
using Letter.Service.Interfaces;
using Letter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Letter.Domain.Helpers;
using System.Security.Claims;
using static Letter.Domain.Helpers.HelperHashCode;
using System.Net.Http;

namespace Letter.Service.Implementations
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository { get; init; }

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public ClaimsIdentity Authentication(User user)
        {
            var claim = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType,user.Email)
            };
            return new ClaimsIdentity(claim, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

       

        public async Task<IBaseResponce<ClaimsIdentity>> LoginUser(User entity)
        {
            var baseResponce = new BaseResponce<ClaimsIdentity>();
            baseResponce.Data = null;
            try
            {
                User user = await userRepository.GetByEmail(entity.Email);
                if (user == null)
                {
                    baseResponce.Code = ResponceCodes.NullTable;
                    baseResponce.Responce = "Пользователь не существует";
                }
                else
                {
                    var checkpass = HashString(entity.Password);
                    if (user.Password != checkpass)
                    {
                        baseResponce.Code = ResponceCodes.WrongPassOrEmail;
                        baseResponce.Responce = "Неверный логин или пароль";
                    }
                    else 
                    {
                        var result = Authentication(user);
                        baseResponce.Data = result;
                        baseResponce.Code= ResponceCodes.Ok;
                    }
                }
                return baseResponce;
            }
            catch (Exception)
            {
                SendErrorMsg("Ошибка проверки логина пользователя");
                return new BaseResponce<ClaimsIdentity>()
                {
                    Data = null,
                    Code = ResponceCodes.UnCatchedError,
                    Responce = "Ошибка проверки логина пользователя"
                };

            }
        }

        public async Task<IBaseResponce<bool>> CreateAsync(User entity)
        {
            var baseResponce = new BaseResponce<bool>();
            try
            {
                var user = await userRepository.Create(entity);
                if (user == true)
                {
                    baseResponce.Code = ResponceCodes.Ok;
                    baseResponce.Data = user;
                    baseResponce.Responce = "Пользователь успешно создан";
                }
                else
                {
                    baseResponce.Code = ResponceCodes.BoolError;
                    baseResponce.Responce = "Ошибка создания пользователя";

                }
                baseResponce.Data = user;
                return baseResponce;
            }
            catch (Exception)
            {
                SendErrorMsg("Ошибка создания пользователя");
                return new BaseResponce<bool>()
                {
                    Data = false,
                    Code = ResponceCodes.UnCatchedError,
                    Responce = "Ошибка создания пользователя"
                };
            }
        }

        public async Task<IBaseResponce<ClaimsIdentity>> CreateNewUserAsync(User user)
        {
            var baseResponce = new BaseResponce<ClaimsIdentity>();
            try
            {
                var checkEmailUser = await userRepository.GetByEmail(user.Email);
                if (checkEmailUser != null)
                {
                    baseResponce.Data = null;
                    baseResponce.Code = ResponceCodes.UserExist;
                    baseResponce.Responce = "Пользователь уже существует";
                    return baseResponce;
                }
                else
                {
                    bool isCreate = (await CreateAsync(user)).Data;
                    if (isCreate)
                    {
                        var result = Authentication(user);
                        baseResponce.Data = result;
                        baseResponce.Code = ResponceCodes.Ok;
                    }
                    
                }
                return baseResponce;
            }
            catch (Exception)
            {
                SendErrorMsg("Ошибка создания пользователя");
                return new BaseResponce<ClaimsIdentity>()
                {
                    Data = null,
                    Code = ResponceCodes.UnCatchedError,
                    Responce = "Ошибка создания пользователя"
                };
            }
        }

        public async Task<IBaseResponce<bool>> DeleteAsync(User entity)
        {
            var baseResponce = new BaseResponce<bool>();
            try
            {
                var user = await userRepository.Delete(entity);
                if (user == true)
                {
                    baseResponce.Code = ResponceCodes.Ok;
                    baseResponce.Data = user;
                    baseResponce.Responce = "Пользователь успешно создан";
                }
                else
                {
                    baseResponce.Code = ResponceCodes.BoolError;
                    baseResponce.Responce = "Ошибка удаления пользователя";

                }
                baseResponce.Data = user;
                return baseResponce;
            }
            catch (Exception)
            {
                SendErrorMsg("Ошибка удаления пользователя");
                return new BaseResponce<bool>()
                {
                    Data = false,
                    Code = ResponceCodes.UnCatchedError,
                    Responce = "Ошибка удаления пользователя"
                };
            }
        }

        public async Task<IBaseResponce<User>> GetByEmailAsync(string email)
        {
            var baseResponce = new BaseResponce<User>();
            try
            {
                var user = await userRepository.GetByEmail(email);
                if (user == null)
                {
                    baseResponce.Code = ResponceCodes.NullTable;
                    baseResponce.Responce = "Пустая таблица";
                }
                else { baseResponce.Code = ResponceCodes.Ok; }
                baseResponce.Data = user;
                return baseResponce;
            }
            catch (Exception)
            {
                return new BaseResponce<User>()
                {
                    Data = null,
                    Code = ResponceCodes.UnCatchedError,
                    Responce = "Ошибка"
                };
            }
        }

        

        public async Task<IBaseResponce<User>> GetByUserNameAsync(string userName)
        {
            var baseResponce = new BaseResponce<User>();
            try
            {
                var user = await userRepository.GetByUserName(userName);
                if (user == null)
                {
                    baseResponce.Code = ResponceCodes.NullTable;
                    baseResponce.Responce = "Пустая таблица";
                }
                else { baseResponce.Code = ResponceCodes.Ok; }
                baseResponce.Data = user;
                return baseResponce;
            }
            catch (Exception)
            {
                return new BaseResponce<User>()
                {
                    Data = null,
                    Code = ResponceCodes.UnCatchedError,
                    Responce = "Ошибка"
                };
            }
        }

        public async Task<IBaseResponce<IEnumerable<User>>> SelectAllAsync()
        {
            var baseResponce = new BaseResponce<IEnumerable<User>>();
            try
            {
                var users = await userRepository.SelectAll();
                if (users == null)
                {
                    baseResponce.Code = ResponceCodes.NullTable;
                    baseResponce.Responce = "Пустая таблица";
                }
                else { baseResponce.Code = ResponceCodes.Ok; }
                baseResponce.Data = users.ToList();
                return baseResponce;
            }
            catch (Exception)
            {
                return new BaseResponce<IEnumerable<User>>()
                {
                    Data = null,
                    Code = ResponceCodes.UnCatchedError,
                    Responce = "Ошибка"
                };
            }
        }

        public async Task<IBaseResponce<User>> SelectByIdAsync(long id)
        {
            BaseResponce<User> baseResponce = new();
            try
            {
                var user = await userRepository.GetById(id);
                if (user == null)
                {
                    baseResponce.Code = ResponceCodes.NullTable;
                    baseResponce.Responce = "Пустая таблица";
                }
                else { baseResponce.Code = ResponceCodes.Ok; }
                baseResponce.Data = user;
                return baseResponce;
            }
            catch (Exception)
            {
                return new BaseResponce<User>()
                {
                    Data = null,
                    Code = ResponceCodes.UnCatchedError,
                    Responce = "Ошибка"
                };
            }
        }

        public void SendErrorMsg(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Возникла непредвиденная ошибка");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
        }

        public async Task<IBaseResponce<bool>> UpdateAsync(User entity)
        {
            var baseResponce = new BaseResponce<bool>();
            try
            {
                var user = await userRepository.Update(entity);
                if (user == true)
                {
                    baseResponce.Code = ResponceCodes.Ok;
                    baseResponce.Data = user;
                    baseResponce.Responce = "Данные обновлены";
                }
                else
                {
                    baseResponce.Code = ResponceCodes.BoolError;
                    baseResponce.Responce = "Ошибка создания пользователя";

                }
                baseResponce.Data = user;
                return baseResponce;
            }
            catch (Exception)
            {
                return new BaseResponce<bool>()
                {
                    Data = false,
                    Code = ResponceCodes.UnCatchedError,
                    Responce = "Ошибка"
                };
            }
        }

    }
}
