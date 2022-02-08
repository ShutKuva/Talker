using Core.DbCreator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    [DbAutoCreator.AutoDB]
    public class RoomUserJoint : BaseEntity
    {
        public RoomUserJoint() { }

        public RoomUserJoint(int roomId, int userId, int customRoleId)
        {
            RoomId = roomId;
            UserId = userId;
            CustomRoleId = customRoleId;
            UserColor = new Random().Next(1, 14);
        }

        [ForeignKey("RoomId")]
        public int RoomId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [ForeignKey("CustomRoleId")]
        public int CustomRoleId { get; set; }

        public int UserColor { get; set; }

        public Room Room { get; set; }
    }
}
