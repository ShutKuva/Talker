using System;
using System.Collections.Generic;
using Core.DbCreator;
namespace Core.Models
{
    [DbAutoCreator.AutoDB]
    public class CustomRole : BaseEntity
    {
        public CustomRole()
        {

        }

        public CustomRole(string roleName, int roleRights)
        {
            SetRoleName(roleName);
            SetRoleRights(roleRights);
        }

        [Flags]
        public enum RoleRights
        {
            None = 0,
            CanInviteUsers = 1,
            CanDeleteUsers = 2,
            CanBanUsers = 4,
            CanDeleteForeignMessages = 8,
            CanDeleteRoom = 16,
        }

        public string RoleName { get; private set; }

        public int CurrentRoleRights { get; private set; }

        public List<RoomUserJoint> RoomUserJoint { get; set; }

        public void SetRoleName(string newRoleName)
        {
            if (newRoleName != null)
            {
                RoleName = newRoleName;
            }
        }

        public void SetRoleRights(int roleRights)
        {
            if (roleRights > 0 && roleRights < 32)
            {
                CurrentRoleRights = roleRights;
            }
            else
            {
                CurrentRoleRights = 0;
            }
        }
    }
}
