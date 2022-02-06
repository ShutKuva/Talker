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
    public class OpenRoom : IPLService
    {
        private readonly Session _openedSession;
        private readonly ICrudService<RoomUserJoint> _crudRoomUser;

        public OpenRoom(Session openSession, ICrudService<RoomUserJoint> crudRoomUser)
        {
            _openedSession = openSession;
            _crudRoomUser = crudRoomUser;
        }

        public async Task Execute(string[] command)
        {
            if (command.Length > 1)
            {
                var roomUser = await _crudRoomUser.ReadWithCondition(x => x.UserId == _openedSession.LoggedUser.Id && x.Room.Name == command[1]);
                if (roomUser.Any())
                {
                    _openedSession.MyLocation = Location.InRoom;
                    _openedSession.RoomId = roomUser.FirstOrDefault().RoomId;
                }
                else
                {
                    Console.WriteLine("Name of room is invalid");
                }
            } else
            {
                Console.WriteLine("Name of room is empty");
            }
        }
    }
}
