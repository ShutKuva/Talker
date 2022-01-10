using System.Collections.Generic;

namespace Core.Models
{
    public class Room : BaseEntity
    {
        private List<User> _users;
        private Dictionary<User, Status> _userStatusPairs;

        public enum Status
        {
            Participant,
            Admin,
        }

        public string Name { get; set; }

        public string ParticipantsNumber { get; set; }

        public bool Locked { get; set; }

        public string InvitationLink { get; set; }

        public List<User> Users { get => _users; set { _users = value; } }

        public Dictionary<User, Status> UserStatusPairs
        {
            get => _userStatusPairs;
            set { _userStatusPairs = value; }
        }

        public Room()
        {
            _users = new List<User>();
        }
    }
}
