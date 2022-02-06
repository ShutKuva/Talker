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
        private readonly ICrudService<RoomUserJoint> _crudRoomUser;
        private readonly ICrudService<Message> _crudMessage;
        private readonly Session _openedSession;

        public WriteMessage(ICrudService<RoomUserJoint> crudRoomUser, ICrudService<Message> crudMessage, Session openedSession)
        {
            _crudRoomUser = crudRoomUser;
            _openedSession = openedSession;
            _crudMessage = crudMessage;
        }

        public async Task Execute(string[] command)
        {
            var roomUser = await _crudRoomUser.ReadWithCondition(x => (x.RoomId == _openedSession.RoomId) && (x.UserId == _openedSession.LoggedUser.Id));
            if (roomUser.Any())
            {
                Console.WriteLine("Write your messange:");
                Message newMessage = new Message(Console.ReadLine(), DateTime.Now, roomUser.FirstOrDefault().Id);
                await _crudMessage.Create(newMessage);
            }
        }
    }
}
