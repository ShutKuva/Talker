using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Room: BaseEntity
    {
        public int ID;
        public string Name;
        public List<User> Users;
        public DateTime CreatedAt;

        public Room() { }

        public Room(int id, string name, DateTime createdAt)
        {
            ID = id;
            Name = name;
            CreatedAt = createdAt;
        }

        public void CreateRole()
        {
            throw new NotImplementedException();
        }
    }
}
