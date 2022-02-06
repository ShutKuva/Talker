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
    public class WriteMessage : IPLService
    {
        private readonly ICrudService<Chat> _chatUser;
        private readonly ICrudService<Message> _crudMessage;
        private readonly Session _openedSession;

        public WriteMessage(ICrudService<Chat> chatUser, ICrudService<Message> crudMessage, Session openedSession)
        {
            _chatUser = chatUser;
            _openedSession = openedSession;
            _crudMessage = crudMessage;
        }

        public async Task Execute(string[] command)
        {
            var chat = await _chatUser.ReadWithCondition(x => (x.Room.Id == _openedSession.RoomId) && (x.Id == _openedSession.ChatId));

            if (chat.Any())
            {
                Console.WriteLine("Write your messange:");
                Message newMessage = new Message(Console.ReadLine(), DateTime.Now, chat.FirstOrDefault().Id);
                await _crudMessage.Create(newMessage);
            }
        }
    }
}
