using Core.DbCreator;

namespace Core.Models
{
    [DbAutoCreator.AutoDB]
    public class RoomUserJoint : BaseEntity
    {
        public int _roomId;
        public int _userId;
        public string _roleName;
        public int _roomRoleId;

        public RoomUserJoint() { }

        public RoomUserJoint(int roomId, int userId, string roleName, int roomRoleId)
        {
            _roomId = roomId;
            _userId = userId;
            _roleName = roleName;
            _roomRoleId = roomRoleId;
        }
    }
}
