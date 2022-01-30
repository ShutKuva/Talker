using Core.DbCreator;

namespace Core.Models
{
    [DbAutoCreator.AutoDB]
    public class RoomRoleJoint : BaseEntity
    {
        public int _roomId;
        public int _roleId;
        public string _roleName;
        public int _roleRights;

        public RoomRoleJoint() { }

        public RoomRoleJoint(int roomId, string roleName, int roleRights)
        {
            _roomId = roomId;
            _roleName = roleName;
            _roleRights = roleRights;
        }
    }
}
