using Core.DbCreator;

namespace Core.Models
{
    [DbAutoCreator.AutoDB]
    public class RoomRoleJoint : BaseEntity
    {
        public string RoleName { get; set; }
        public int RoleRights { get; set; }

        public RoomRoleJoint() { }

        public RoomRoleJoint(string roleName, int roleRights)
        {
            RoleName = roleName;
            RoleRights = roleRights;
        }
    }
}
