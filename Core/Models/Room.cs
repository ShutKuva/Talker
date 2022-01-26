using System;

namespace Core.Models
{
    public class Room: BaseEntity
    {
        public int ID;
        public string Name;
        public DateTime CreatedAt;

        public Room() { }

        public Room(int id, string name, DateTime createdAt)
        {
            ID = id;
            Name = name;
            CreatedAt = createdAt;
        }
    }
}
