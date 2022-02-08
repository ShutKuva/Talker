﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface ICustomRoleService
    {
        Task<bool> CreateNewRole(CustomRole customRole);

        Task<bool> ModifyRole(int customRoleId, string newRoleName = null, CustomRole.RoleRights? newRoleRights = null);

        Task<bool> DeleteRole(int customRoleId);

        Task<CustomRole> GetRoomRoleJoint(int roomRoleId);

        Task<IEnumerable<CustomRole>> ReadWithCondition(Expression<Func<CustomRole, bool>> expression);
    }
}
