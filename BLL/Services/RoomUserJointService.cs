using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RoomUserJointService : IRoomUserJointService
    {
        private readonly IGenericRepository<RoomUserJoint> _roomUserJointRepository;
        private readonly IRoomRoleJointService _roomRoleJointService;
        private readonly ICrudService<Room> _roomCrud;
        private readonly ICrudService<User> _userCrud;

        public RoomUserJointService(
            IGenericRepository<RoomUserJoint> roomUserJointRepository,
            IRoomRoleJointService roomRoleJointService,
            ICrudService<Room> roomCrud,
            ICrudService<User> userCrud)
        {
            _roomUserJointRepository = roomUserJointRepository;
            _roomRoleJointService = roomRoleJointService;
            _roomCrud = roomCrud;
            _userCrud = userCrud;
        }

        public async Task<bool> CreateNewUserInRoom(int roomId, int userId, int roleId)
        {
            var rooms = _roomCrud.ReadWithCondition(x => x.Id == roomId);
            var users = _userCrud.ReadWithCondition(x => x.Id == userId);
            var roles = _roomRoleJointService.ReadWithCondition(x => x.Id == roleId);

            await Task.WhenAll(rooms, users, roles);

            var room = rooms.Result.FirstOrDefault();
            var user = users.Result.FirstOrDefault();
            var role = roles.Result.FirstOrDefault();

            if (room == null || user == null || role == null)
            {
                return false;
            }

            RoomUserJoint roomUserJoint = new RoomUserJoint(roomId, userId, roleId);

            await _roomUserJointRepository.CreateAsync(roomUserJoint);
            return true;
        }

        public async Task<bool> AssignRoleToUser(int roomUserId, int roomRoleId)
        {
            var roomUserJoint = await this.GetRoomUserJoint(roomUserId);

            if (roomUserJoint == null)
            {
                return false;
            }

            roomUserJoint.RoomRoleId = roomRoleId;

            await _roomUserJointRepository.UpdateAsync(roomUserJoint);
            return true;
        }

        public async Task<bool> DeleteUserFromRoom(int roomUserId)
        {
            var roomUserJoint = await this.GetRoomUserJoint(roomUserId);

            if (roomUserJoint == null)
            {
                return false;
            }

            await _roomUserJointRepository.DeleteAsync(roomUserJoint);
            return true;
        }

        public async Task<RoomUserJoint> GetRoomUserJoint(int roomUserId)
        {
            var roomUserJoint = await this.ReadWithCondition(x => x.Id == roomUserId);
            var roomUser = roomUserJoint.FirstOrDefault();

            return roomUser;
        }

        public async Task<IEnumerable<RoomUserJoint>> ReadWithCondition(Expression<Func<RoomUserJoint, bool>> expression) // добавлен доп метод для удобства
        {
            var data = await _roomUserJointRepository.FindByConditionAsync(expression);

            if (data.Any())
            {
                return data.ToList();
            }

            return null;
        }
    }
}
