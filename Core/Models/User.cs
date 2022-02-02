using Core.DbCreator;
using System.Collections.Generic;

namespace Core.Models
{
    [DbAutoCreator.AutoDB]
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public List<RoomUserJoint> RoomUser { get; set; }

        public User()
        {

        }

        public User(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            Age = user.Age;
            Password = user.Password;
            Username = user.Username;
        }
    }
}