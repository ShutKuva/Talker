using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Chat : BaseEntity
    {
        public string Name { get; set; }

        [ForeignKey("RoomId")]
        public int RoomId { get; set; }

        public List<Message> Message { get; set; }

        public Chat()
        {

        }

        public Chat(string name, int roomId)
        {
            Name = name;
            RoomId = roomId;
        }
    }
}
