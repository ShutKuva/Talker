using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface IRoomRoleJointService
    {
        Task<bool> CreateNewRole(Room room, CustomRole customRole);

        Task<bool> ModifyRole(Room room, int customRoleId, string newRoleName = null, CustomRole.RoleRights? newRoleRights = null);

        Task<bool> DeleteRole(Room room, int customRoleId);

        Task<IEnumerable<RoomRoleJoint>> ReadWithCondition(Expression<Func<RoomRoleJoint, bool>> expression);
    }
}
