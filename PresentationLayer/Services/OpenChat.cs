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

        public OpenChat(ICrudService<Chat> crudChat, Session openedSession)
        {
            _crudChat = crudChat;
            _openedSession = openedSession;
        }

        public async Task Execute(string[] command)
        {
            if (command.Length > 1)
            {
                var chat = await _crudChat.ReadWithCondition(x => x.RoomId == _openedSession.ChatId && x.Name == command[1]);

                if (chat.Any())
                {
                    _openedSession.MyLocation = Location.InChat;
                    _openedSession.ChatId = chat.FirstOrDefault().Id;
                }
                else
                {
                    Console.WriteLine("Name of room is invalid");
                }
            } else
            {
                Console.WriteLine("Name of chat is empty");
            }
        }
    }
}
