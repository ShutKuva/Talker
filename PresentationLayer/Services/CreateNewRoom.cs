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
    public class CreateNewRoom : IPLService
    {
        private readonly CustomRole.RoleRights defaultAdminRoleRigths = (CustomRole.RoleRights) 31;
        private readonly ICrudService<Room> _crudRoom;
        private readonly ICrudService<User> _crudUser;
        private readonly ICrudService<CustomRole> _crudRole;
        private readonly IRoomUserJointService _roomUserJointService;
        private readonly Session _openedSession;

        public CreateNewRoom(
            ICrudService<Room> crudRoom,
            ICrudService<User> crudUser,
            ICrudService<CustomRole> crudRole,
            IRoomUserJointService roomUserJointService,
            Session openedSession)
        {
            _crudRoom = crudRoom;
            _crudUser = crudUser;
            _crudRole = crudRole;
            _roomUserJointService = roomUserJointService;
            _openedSession = openedSession;
        }

        public async Task Execute(string[] command)
        {
            var room = new Room();
            //{
            //    Id = new Random().Next(10000, 99999),
            //};

            Console.WriteLine("Enter a room name: ");
            room.Name = Console.ReadLine();

            bool success;
            do
            {
                success = await _crudRoom.Create(room, x => x.Name == room.Name);
                if (!success)
                {
                    Console.WriteLine("This name is already exists, try another:");
                    room.Name = Console.ReadLine();
                }
            } while (!success);

            room.CreatedAt = DateTime.Now;

            var user = _openedSession.LoggedUser;

            var adminRole = new CustomRole("Admin", (int) defaultAdminRoleRigths);

            await _crudRole.Create(adminRole);

            await _roomUserJointService.CreateNewUserInRoom(room.Id, user.Id, adminRole.Id);

            var roomUserJoints = await _roomUserJointService.ReadWithCondition(x => x.UserId == user.Id && x.RoomId == room.Id);
            var roomUserJoint = roomUserJoints.FirstOrDefault();

            room.RoomUsers = new List<RoomUserJoint>()
            {
                await _roomUserJointService.GetRoomUserJoint(roomUserJoint.Id),
            };

            _openedSession.MyLocation = Location.InRoom;
            _openedSession.RoomId = room.Id;
        }
    }
}
