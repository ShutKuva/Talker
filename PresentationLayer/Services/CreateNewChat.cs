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
    public class CreateNewChat : IPLService
    {
        private readonly ICrudService<Chat> _crudChat;
        private readonly Session _openedSession;

        public CreateNewChat(ICrudService<Chat> crudChat, Session openedSession)
        {
            _crudChat = crudChat;
            _openedSession = openedSession;
        }

        public async Task Execute(string[] command)
        {
            string name;
            if (command.Length > 1)
            {
                name = command[1];
            } else
            {
                name = GetName();
            }

            var chat = new Chat(name, _openedSession.RoomId);
            bool success;
            do
            {
                success = await _crudChat.Create(chat, x => x.RoomId == chat.RoomId && x.Name == chat.Name);
                if (!success)
                {
                    name = GetName();
                }
            } while (!success);
        }

        private string GetName()
        {
            string name;
            do
            {
                Console.WriteLine("Name is invalid or already used, please print another:");
                name = Console.ReadLine();
            } while (String.IsNullOrEmpty(name));

            return name;
        }
    }
}
