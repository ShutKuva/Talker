using System;
using System.Collections.Generic;
using Core.DbCreator;

namespace Core.Models
{
    [DbAutoCreator.AutoDB]
    public class Room : BaseEntity
    {
        public string Name { get; set; }

        public int IdOfDefaultRole { get; set; }

        public List<RoomUserJoint> RoomUsers { get; set; }

        public List<Chat> Chats { get; set; }

        public DateTime CreatedAt { get; set; }

        public Room() { }

        public Room(string name, DateTime createdAt)
        {
            Name = name;
            CreatedAt = createdAt;
        }

        public Room(Room room)
        {
            Name = room.Name;
            CreatedAt = room.CreatedAt;
        }
    }
}
