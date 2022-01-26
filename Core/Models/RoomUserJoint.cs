namespace Core.Models
{
    public class RoomUserJoint
    {
        public int ID;
        public int RoomID;
        public int UserID;
        public int StatusID;
        public int RoomRoleID; // ?

        public RoomUserJoint() { }

        public RoomUserJoint(int id, int roomId, int userId, int statusId, int roomRoleId)
        {
            ID = id;
            RoomID = roomId;
            UserID = userId;
            StatusID = statusId;
            RoomRoleID = roomRoleId;
        }
    }
}
