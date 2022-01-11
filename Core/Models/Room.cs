using System.Collections.Generic;
using System.Linq;

namespace Core.Models
{
    public class Room : BaseEntity
    {
        private List<User> _users;
        private Dictionary<User, Status> _userStatusPairs;
        private Dictionary<User, List<CustomRole>> _userRolePairs;
        private List<RoomSettings> settings;

        public readonly List<CustomRole> Roles;

        public enum RoomSettings
        {
            Private,
            Public
        }

        public enum Status
        {
            Participant,
            Admin,
        }

        public class CustomRole
        {
            public string Name;
            public List<Capability> Capabilities;

            public CustomRole(string name, List<Capability> settings)
            {
                Name = name;
                Capabilities = settings;
            }
        }

        public enum Capability
        {
            CanChangeRoomName,
            CanAddNewUsers,
            CanChangeIcon,
            CanDeleteUserMessages
        }

        public string Name { get; set; }

        public int ParticipantsNumber { get => _users.Count; }

        public string InvitationLink { get; set; }

        public List<User> Users { get => _users; set { _users = value; } }

        public Dictionary<User, Status> UserStatusPairs
        {
            get => _userStatusPairs;
            set { _userStatusPairs = value; }
        }

        public Dictionary<User, List<CustomRole>> UserCapabilityPairs
        {
            get => _userRolePairs;
            set { _userRolePairs = value; }
        }

        public Room()
        {
            _users = new();
            _userRolePairs = new();
            _userStatusPairs = new();
            Roles = new();

            settings.Add(RoomSettings.Public);
        }

        public Room(params RoomSettings[] settings): this()
        {
            foreach (var setting in settings)
            {
                if (!this.settings.Contains(setting))
                    this.settings.Add(setting);
            }
        }

        public void HandleCapalities(List<Capability> userCapabilites) // needs to be async
        {
            foreach (var capability in userCapabilites)
            {
                switch (capability)
                {
                    case Capability.CanAddNewUsers:
                        break;
                    case Capability.CanChangeIcon:
                        break;
                    case Capability.CanChangeRoomName:
                        break;
                    case Capability.CanDeleteUserMessages:
                        break;
                }
            }
        }

        public bool CreateRole(string name, List<Capability> capabilities) // needs to be async
        {
            CustomRole newRole = new(name, capabilities);
            var role = Roles.Where(x => x.Name == newRole.Name).FirstOrDefault();

            if (role != null)
            {
                return false;
            }

            Roles.Add(role);

            return true;
        }

        public bool SetRole(User user, CustomRole role) // needs to be async
        {
            var pair = _userRolePairs.Where(x => x.Key.Id == user.Id).FirstOrDefault();

            if(pair.Key == null || pair.Value.Equals(role) /** will be moved to separate if block */ ) 
            {
                return false; // here will be a enum with different error types but for now just bool
            }

            _userRolePairs.TryAdd(pair.Key, pair.Value);

            return true;
        }
    }
}
