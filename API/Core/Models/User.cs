using Core.DbCreator;
using Core.Models.MiniModels;
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
        public List<RoomUserJoint> RoomUser { get; set; } = new List<RoomUserJoint>();

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

        public User(RegisterModel regModel)
        {
            Name = regModel.Name;
            Surname = regModel.Surname;
            Age = regModel.Age;
            Password = regModel.Password;
            Username = regModel.Username;
        }
    }
}