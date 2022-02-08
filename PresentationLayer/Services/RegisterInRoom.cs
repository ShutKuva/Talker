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
    public class RegisterInRoom : IPLService
    {
        private readonly IRoomUserJointService _crudRoomUser;
        private readonly ICrudService<Room> _crudRoom;
        private readonly Session _openedSession;

        public RegisterInRoom(IRoomUserJointService crudRoomUser, ICrudService<Room> crudRoom, Session openedSession)
        {
            _crudRoomUser = crudRoomUser;
            _openedSession = openedSession;
            _crudRoom = crudRoom;
        }

        public async Task Execute(string[] command)
        {
            if (command.Length > 1)
            {
                var existedRoom = await _crudRoom.ReadWithCondition(x => x.Name == command[1]);
                if (existedRoom?.Any() ?? false)
                {
                    var alreadyRegistered = await _crudRoomUser.ReadWithCondition(x => x.Room.Name == command[1] && x.UserId == _openedSession.LoggedUser.Id);
                    if (alreadyRegistered == null)
                    {
                        await _crudRoomUser.CreateNewUserInRoom(existedRoom.FirstOrDefault().Id, _openedSession.LoggedUser.Id, existedRoom.FirstOrDefault().IdOfDefaultRole);
                        _openedSession.MyLocation = Location.InRoom;
                        _openedSession.RoomId = existedRoom.FirstOrDefault().Id;
                        Console.WriteLine("Succesfully registered in room!");
                    }
                    else
                    {
                        Console.WriteLine("You already registered in this room");
                    }
                }
                else
                {
                    Console.WriteLine("Room with name does not existed");
                }
            } else
            {
                Console.WriteLine("Write a room name after command, please");
            }
        }
    }
}
