using System;
namespace Core.Models
{
    public class CustomRole : BaseEntity
    {
        public CustomRole(string roleName, RoleRights roleRights)
        {
            SetRoleName(roleName);
            CurrentRoleRights = roleRights;
        }

        [Flags]
        public enum RoleRights
        {
            None = 0,
            InvitingToRoom = 1,
            DelitingFromRoom = 2,
            Banning = 4,
            DelitingOthersMessages = 8,
            DelitingRoom = 16,
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
            CurrentRoleRights = roleRights;
        }
    }
}
