using System;
namespace Core.Models
{
    public class Permission
    {
        public enum Capability
        {
            CanChangeRoomName,
            CanAddNewUsers,
            CanChangeIcon,
            CanDeleteUserMessages
        }

        public int ID;
        public Capability Ability;

        public Permission() { }
        public Permission(int id, Capability ability)
        {
            ID = id;
            Ability = ability;
        }
    }
}
