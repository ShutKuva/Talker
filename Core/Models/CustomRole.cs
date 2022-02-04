using System;
using Core.DbCreator;
namespace Core.Models
{
    [DbAutoCreator.AutoDB]
    public class CustomRole : BaseEntity
    {
        public CustomRole(string roleName, RoleRights roleRights)
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

        public RoleRights CurrentRoleRights { get; private set; }

        public void SetRoleName(string newRoleName)
        {
            if (newRoleName != null)
            {
                RoleName = newRoleName;
            }
        }

        public void SetRoleRights(RoleRights roleRights)
        {
            if (roleRights > 0 && roleRights < (RoleRights) 32)
            {
                CurrentRoleRights = roleRights;
            }

            CurrentRoleRights = 0;
        }
    }
}
