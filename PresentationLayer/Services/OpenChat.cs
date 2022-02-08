using BLL.Abstractions.Interfaces;
using Core.Models;
using PresentationLayer.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    class OpenChat : IPLService
    {
        private readonly Session _openedSession;
        private readonly ICrudService<Chat> _crudChat;
        private readonly ICrudService<Message> _crudMessage;
        private readonly ICrudService<User> _crudUser;

        public OpenChat(ICrudService<Chat> crudChat, ICrudService<Message> crudMessage, ICrudService<User> crudUser, Session openedSession)
        {
            _crudChat = crudChat;
            _crudUser = crudUser;
            _crudMessage = crudMessage;
            _openedSession = openedSession;
        }

        public async Task Execute(string[] command)
        {
            if (command.Length > 1)
            {
                var chat = await _crudChat.ReadWithCondition(x => x.RoomId == _openedSession.RoomId && x.Name == command[1]);

                if (chat?.Any() ?? false)
                {
                    _openedSession.MyLocation = Location.InChat;
                    _openedSession.ChatId = chat.FirstOrDefault().Id;
                    await GetAll();
                }
                else
                {
                    Console.WriteLine("Name of chat is invalid");
                }
            } else
            {
                Console.WriteLine("Name of chat is empty");
            }
        }

        private async Task GetAll()
        {
            var messages = await _crudMessage.ReadWithCondition(x => x.ChatId == _openedSession.ChatId);
            if (messages != null)
            {
                foreach (Message message in messages)
                {
                    var users = await _crudUser.ReadWithCondition(x => x.Id == message.UserId);
                    Console.WriteLine($"\"{users.FirstOrDefault().Username}\"");
                    Console.WriteLine($"\t{message.Text}\n");
                }
            } else
            {
                Console.WriteLine("Chat is empty");
            }
        }
    }
}
