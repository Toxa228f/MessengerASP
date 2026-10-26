using Letter.Domain.Entities;
using Letter.Service.Implementations;
using Letter.Service.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Models
{
    public class ChatHub : Hub, IChatHub
    {
        private readonly IChatMessagesService msgSer;
        private readonly IChatService chtSer;

        private readonly IPrivateChatMessagesService pmsgSer;
        private readonly IPrivateChatsService pchtSer;

        public ChatHub(IChatMessagesService msgSer, IChatService chtSer, IPrivateChatMessagesService pmsgSer, IPrivateChatsService pchtSer)
        {
            this.msgSer = msgSer;
            this.chtSer = chtSer;
            this.pmsgSer = pmsgSer;
            this.pchtSer = pchtSer;
        }

        public async Task SendMessageAll(string user, string message, string datatime)
        {
            await Clients.All.SendAsync("ReceiveMessageToAll", user, message, datatime);
        }

        public async Task SendMessageGroup(string groopName, string userName, string message, string datatime, long chatId)
        {
            await Clients.Group(groopName).SendAsync("ReceiveMessageGroup", message, userName, datatime);

            string[] x = datatime.Split(' ');
            string time = x[1] + " " + x[2] + " " + (x[4].Split(':')[0] + ":" + x[4].Split(':')[1]);

            ChatMessage msg = new ChatMessage()
            {
                MessageText = message,
                ChatId = chatId,
                DataTime = time,
                UserName = userName,
            };
            StringBuilder saveMsg = new();
            saveMsg.Append(userName);
            saveMsg.Append(":");
            saveMsg.Append(message);
            if (userName.Length + message.Length + 1 >= 23)
            {
                saveMsg[20] = '.';
                saveMsg[21] = '.';
                saveMsg[22] = '.';
                int len = saveMsg.Length - 23;
                saveMsg.Remove(23, len);
            }
            await chtSer.AddNewMessage(chatId, saveMsg.ToString());
            await msgSer.CreateAsync(msg);
        }

        public async Task SendMessagePrivateChat(string getterName, string senderName, string message, string datatime, long privateChatId)
        {
            await Clients.Users(getterName).SendAsync("ReceiveMessagePrivateChat", message, "", datatime);

            string[] x = datatime.Split(' ');
            string time = x[1] + " " + x[2] + " " + (x[4].Split(':')[0] + ":" + x[4].Split(':')[1]);

            PrivateChatMessage msg = new PrivateChatMessage()
            {
                MessageText = message,
                PrivateChatId = privateChatId,
                DataTime = time,
                UserName = senderName,
            };
            StringBuilder saveMsg = new();
            saveMsg.Append(senderName);
            saveMsg.Append(":");
            saveMsg.Append(message);
            if (senderName.Length + message.Length + 1 >= 23)
            {
                saveMsg[20] = '.';
                saveMsg[21] = '.';
                saveMsg[22] = '.';
                int len = saveMsg.Length - 23;
                saveMsg.Remove(23, len);
            }
            await pchtSer.AddNewMessage(privateChatId, saveMsg.ToString());
            await pmsgSer.CreateAsync(msg);
        }

        public async Task AddUserInChat(string groopName, string userName, string datatime)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groopName);

            await Clients.Group(groopName).SendAsync("ReceiveSystemMessageGroup", $"Пользователь {userName} присоединился", userName, datatime);
        }

        public async Task AddUserInPrivateChat(string groopName)
        {
            await base.OnConnectedAsync();
        }

        public async Task DelUserFromChat(string groopName, string userName, string datatime)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groopName);

            await Clients.Group(groopName).SendAsync("ReceiveSystemMessageGroup", $"Пользователь {userName} вышел", datatime);
        }
    }

    public interface IChatHub
    {
        public Task SendMessageAll(string user, string message, string datatime);
        public Task SendMessageGroup(string user, string message, string datatime, string groopName, long chatId);
        public Task AddUserInChat(string groopName, string userName, string datatime);
        public Task DelUserFromChat(string groopName, string userName, string datatime);
    }
}
