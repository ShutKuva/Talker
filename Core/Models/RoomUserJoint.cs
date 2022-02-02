using Core.DbCreator;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    [DbAutoCreator.AutoDB]
    public class RoomUserJoint : BaseEntity
    {
        [ForeignKey("RoomId")]
        public int RoomId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public string RoleName { get; set; }

        public int RoomRoleId { get; set; }

        public RoomUserJoint() { }

        public RoomUserJoint(int roomId, int userId, string roleName, int roomRoleId)
        {
            RoomId = roomId;
            UserId = userId;
            RoleName = roleName;
            RoomRoleId = roomRoleId;
        }
    }
}
