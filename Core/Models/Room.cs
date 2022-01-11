using System.Collections.Generic;

namespace Core.Models
{
    public class Room : BaseEntity
    {
        public Room()
        {
            _users = new List<User>();
        }

        public enum Status
        {
            Participant,
            Admin,
        }

        public List<User> _users;
        public Dictionary<User, Status> _userStatusPairs;

        public string Name { get; set; }

        public string ParticipantsNumber { get; private set; }

        public bool Locked { get; private set; }

        public string InvitationLink { get; private set; }

        public List<User> Users { get => _users; set { _users = value; } }

        public Dictionary<User, Status> UserStatusPairs
        {
            get => _userStatusPairs;
            set { _userStatusPairs = value; }
        }
    }
}
