using Core.DbCreator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    [DbAutoCreator.AutoDB]
    public class Message : BaseEntity
    {
        public Message()
        {

        }

        public Message(string text, DateTime writtenAt, int roomUserJointId)
        {
            Text = text;
            WrittenAt = writtenAt;
            RoomUserJointId = roomUserJointId;
        }

        [Required]
        [MaxLength(4096)]
        public string Text { get; set; }

        public DateTime WrittenAt { get; set; }

        [ForeignKey("RoomUserJointId")]
        public int RoomUserJointId { get; set; }
    }
}
