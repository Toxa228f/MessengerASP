using Letter.Domain.Entities;
using Letter.Domain.Enums;
using Letter.Models;
using Letter.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Letter.Controllers
{
    public class ChatsController : Controller
    {
        private readonly IUserInChatService userInChatService;
        private readonly IChatService chatService;
        private readonly IUserService usser;
        private readonly IChatMessagesService messagesService;

        public ChatsController(IUserInChatService userInChatService, IChatService chatService, IUserService usser, IChatMessagesService messagesService)
        {
            this.userInChatService = userInChatService;
            this.chatService = chatService; 
            this.usser = usser;
            this.messagesService = messagesService;
        }

        [Authorize]
        public async Task<IActionResult> Index(ChatInfoViewModel info)
        {
            var data = await chatService.GetChatByUserInChatByUserIdAsync(Convert.ToInt64(HttpContext.Session.GetString("UserId")));
            var messages = await messagesService.GetAllByChatId(info.Id);

            return View((data.Data.ToList(), info, messages.Data.ToList()));
        }

        [Authorize]
        public IActionResult GlobalChat()
        {
            return View();
        }

        [Authorize]
        public IActionResult NewChat()
        {
            return View();
        }

        public async Task<IActionResult> SelectChat(int id)
        {
            var data = await userInChatService.IsUserInChatWithUserIdAndChatId(Convert.ToInt64(HttpContext.Session.GetString("UserId")), id);
            if (data != null)
            {
                if (data.Code == ResponceCodes.Ok)
                {
                    var chatdata = await chatService.SelectByIdAsync(id);

                    var userdata = await usser.SelectByIdAsync(Convert.ToInt64(HttpContext.Session.GetString("UserId")));

                    ChatInfoViewModel info = new ChatInfoViewModel()
                    {
                        Id = id,
                        ChatName = chatdata.Data.Name,
                        UserName = userdata.Data.Login,
                        ChatCode = chatdata.Data.ChatCode,
                        LastMessage = chatdata.Data.LastMessage
                    };
                    
                    return RedirectToAction("Index", info);
                }
            }

            return RedirectToActionPermanent("Index");
            /*
               //\\ \ \ \ \/\/ / / /
              ///\\\ \ \ \/  \/ / /
             ////\\\\ \ \/    \/ /
            /////\\\\\ \/      \/
            */

        }

        [Authorize]
        public async Task<IActionResult> EnterByAddres(string code)
        {
            var data = await chatService.GetByChatCodeHashAsync(code);
            if (data.Code == ResponceCodes.Ok)
            {
                var user = await usser.SelectByIdAsync(Convert.ToInt64(HttpContext.Session.GetString("UserId")));
                var x = new UserInChat()
                {
                    UserId = user.Data.Id,
                    ChatId = data.Data.Id
                };
                var responce = await userInChatService.CreateAsync(new UserInChat { UserId = user.Data.Id, ChatId = data.Data.Id });
                return RedirectToActionPermanent("Index");
            }
            return RedirectToActionPermanent("EnterChat");
        }


        public async Task<IActionResult> EnterByCode(string code)
        {
            var data = await chatService.GetByChatCodeAsync(code);
            if (data.Code==ResponceCodes.Ok)
            {
                var user = await usser.SelectByIdAsync(Convert.ToInt64(HttpContext.Session.GetString("UserId")));
                var x = new UserInChat()
                {
                    UserId = user.Data.Id,
                    ChatId = data.Data.Id
                };
                var responce = await userInChatService.CreateAsync(new UserInChat { UserId = user.Data.Id, ChatId = data.Data.Id}) ;
                return RedirectToActionPermanent("Index");
            }
            return RedirectToActionPermanent("EnterChat");
        }

        public ActionResult EnterChat()
        {
            return View();
        }


        public IActionResult ShowCode(object code)
        {
            return View(code);
        }


        public async Task<IActionResult> CreateChat(ChatModel model)
        {
            if (ModelState.IsValid)
            {
                var chat = new Chat()
                {
                    Name = model.Name,
                    Description = model.Description,
                    //Image = model.Image,
                };
                var responce = await userInChatService.CreateUserInChatWithNewChat(Convert.ToInt64(HttpContext.Session.GetString("UserId")), chat);
                if (responce != null)
                {
                    switch (responce.Code)
                    {
                        case ResponceCodes.Ok:
                            return View("ShowCode", responce.Responce);
                        case ResponceCodes.BoolError:
                            ModelState.AddModelError("Email", "Ошибка создания чата, проверьте правильность введённых данных и повторите попытку позже");
                            return View("NewChat");
                    }
                }

            }
            return View("NewChat");
        }
    }
}
