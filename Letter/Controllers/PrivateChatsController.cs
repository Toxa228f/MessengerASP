using Letter.Domain.Entities;
using Letter.Domain.Enums;
using Letter.Domain.ViewModels;
using Letter.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Letter.Controllers
{
    public class PrivateChatsController : Controller
    {
        private readonly IUserService serU;
        private readonly IPrivateChatsService serPC;
        private readonly IPrivateChatMessagesService serPCM;

        public PrivateChatsController(IUserService serU, IPrivateChatsService serPC, IPrivateChatMessagesService serPCM)
        {
            this.serU = serU;
            this.serPC = serPC;
            this.serPCM = serPCM;
        }

        [Authorize]
        public async Task<IActionResult> Index(PrivateChatInfoViewModel? chat)
        {
            long id = Convert.ToInt64(HttpContext.Session?.GetString("UserId"));
            List<PrivateChat> chats = (await serPC.GetByFirstUserId(id))?.Data.ToList();
            List<PrivateChat> chats2 = (await serPC.GetBySecondUserId(id))?.Data?.ToList();
            if (chats2?.Count>0)
            {
                chats.AddRange(chats2);
            }

            List<PrivateChatMessage> messages = (await serPCM.GetAllByChatId((long)chat?.Id))?.Data?.ToList();

            return View((chats, chat, messages));
        }

        public async Task<IActionResult> SelectChat(int id)
        {
            if (id!=0)
            {
                PrivateChat chat1 = (await serPC.SelectByIdAsync(id))?.Data;

                var me = (await serU.SelectByIdAsync(Convert.ToInt64(HttpContext.Session?.GetString("UserId"))))?.Data;

                PrivateChatInfoViewModel chat = new()
                {
                    Id = chat1.Id,
                    Name = chat1.Name,
                    Code = chat1.Code,
                    Role = chat1.Role,
                    MessagesCount = chat1.MessagesCount,
                    MyUserName = me.Login
                };
                return RedirectToActionPermanent("Index",chat);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult NewChat()
        {
            return View();
        }

        public async Task<IActionResult> CreateNewPrivateChat(NewPrivateChatViewModel model) 
        {
            if (ModelState.IsValid)
            {
                var secondUser = (await serU.GetByEmailAsync(model.Email))?.Data;
                var me = (await serU.SelectByIdAsync(Convert.ToInt64(HttpContext.Session?.GetString("UserId"))))?.Data;
                if (secondUser != null&& me != null) 
                {
                    var resconce = await serPC.CreateNewAsync(new PrivateChat()
                    {
                        UserFirstId = me.Id,
                        UserSecondId = secondUser.Id,
                        Name = secondUser.Login,
                        Role = PrivateChatRole.User,
                    });
                    switch (resconce.Code)
                    {
                        case Domain.Enums.ResponceCodes.Ok:
                            return Redirect("Index");
                        default:
                            ModelState.AddModelError("Email", resconce.Responce);
                            return View("NewChat");
                    }
                }
                ModelState.AddModelError("Email","Пользователь не найден");
            }
            return View("NewChat");
        }
    }
}
