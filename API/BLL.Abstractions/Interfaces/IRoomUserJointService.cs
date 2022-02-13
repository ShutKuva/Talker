using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface IRoomUserJointService
    {
        Task<bool> CreateNewUserInRoom(int roomId, int userId, int roleId);

        Task<bool> AssignRoleToUser(int roomUserId, int roomRoleId);

        Task<RoomUserJoint> GetRoomUserJoint(int roomUserId);

        Task<IEnumerable<RoomUserJoint>> ReadWithCondition(Expression<Func<RoomUserJoint, bool>> expression);
    }
}
