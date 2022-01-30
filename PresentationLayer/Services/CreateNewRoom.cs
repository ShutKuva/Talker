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
        private readonly IRoomUserJointService _roomUserJointService;
        private readonly IRoomRoleJointService _roomRoleJointService; 
        private readonly Session _openedSession;

        public CreateNewRoom(ICrudService<Room> crudRoom, IRoomUserJointService roomUserJointService, IRoomRoleJointService roomRoleJointService, Session openedSession)
        {
            _crudRoom = crudRoom;
            _roomUserJointService = roomUserJointService;
            _roomRoleJointService = roomRoleJointService;
            _openedSession = openedSession;
        }

        public async Task Execute(string[] command)
        {
            Room room = new Room()
            {
                Id = new Random().Next(10000, 99999),
            };

            Console.WriteLine("Enter a room name: ");
            room.Name = Console.ReadLine();

            room.CreatedAt = DateTime.Now;

            room.Users = new List<User>()
            {
                _openedSession.LoggedUser,
            };

            CustomRole adminRole = new CustomRole("Admin", defaultAdminRoleRigths);
            await _roomRoleJointService.CreateNewRole(room, adminRole);

            await _crudRoom.Create(room, x => true);

            _roomUserJointService.
        }
    }
}
