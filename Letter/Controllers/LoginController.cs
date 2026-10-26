using Letter.Domain.Entities;
using Letter.Domain.Enums;
using Letter.Domain.Helpers;
using Letter.Models;
using Letter.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Letter.Controllers
{
    public class LoginController : Controller
    {
        private IUserService userService { get; set; }
        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }

        
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Log()
        {
            return View();
        }

        public async Task<IActionResult> EnterUser(UserViewModel model)
        {
            if (model.Email != null && model.Password != null)
            {
                var user = new User()
                {
                    Login = model.Name,
                    Email = model.Email,
                    Location = model.Location,
                    BornDate = model.BornDate,
                    Password = model.Password,
                };
                var data = await userService.LoginUser(user);
                
                if (data != null)
                {
                    if (data.Code!=ResponceCodes.Ok)
                    { 
                        ModelState.AddModelError("Email", data.Responce);
                    }
                    else
                    {
                        var usr = await userService.GetByEmailAsync(user.Email);
                        HttpContext.Session.SetString("UserId", Convert.ToString(usr.Data.Id)); 

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(data.Data));
                        return Redirect("/Home/Index");
                    }
                }
            }
            return View("Log");
        }


        public async Task<IActionResult> ExitUser(UserViewModel model)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Log");
        }


        public async Task<IActionResult> CreateNewUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = new User()
                {
                    Login = model.Name,
                    Email = model.Email,
                    Location = model.Location,
                    BornDate = model.BornDate,
                    Password = HelperHashCode.HashString(model.Password)
                };
                var data = await userService.CreateNewUserAsync(user);
                if (data.Data != null)
                {
                    switch (data.Code)
                    {
                        case ResponceCodes.Ok:
                            var usr = await userService.GetByEmailAsync(user.Email);
                            HttpContext.Session.SetString("UserId", Convert.ToString(usr.Data.Id));

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(data.Data));

                            return Redirect("/Chats/Index");
                        case ResponceCodes.UserExist:
                            ModelState.AddModelError("Email", "Пользователь уже существует");
                            return View("Index");
                        case ResponceCodes.BoolError:
                            ModelState.AddModelError("Login", "Ошибка создания пользователя");
                            return View("Index");
                    }
                }

            }
            return View("Index");
        }
    }
}
