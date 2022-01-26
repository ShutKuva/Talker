namespace Core.Models
{
    public class RoomRoleJoint : BaseEntity
    {
        public int ID;
        public int RoomID;
        public string RoleName;
        public int PermissionID;

        public RoomRoleJoint() { }

        public RoomRoleJoint(int id, int roomId, string roleName, int permissionId)
        {
            ID = id;
            RoomID = roomId;
            RoleName = roleName;
            PermissionID = permissionId;
        }
    }
}
