using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Room : BaseEntity
    {
        public string _name;
        public List<User> _users;
        public DateTime _createdAt;

        public Room() { }

        public Room(string name, DateTime createdAt)
        {
            _name = name;
            _createdAt = createdAt;
        }
    }
}
