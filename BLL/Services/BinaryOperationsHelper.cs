using static Core.Models.CustomRole;

namespace BLL.Services
{
    public class BitOperationsHelper
    {
        public RoleRights Value { get; private set; }
        public BitOperationsHelper() => this.Value = RoleRights.None;
        public BitOperationsHelper(RoleRights value) => this.Value = value;
        public void Add(RoleRights value) => this.Value |= value;
        public void Remove(RoleRights value) => this.Value ^= value;
        public bool Contains(RoleRights value) => (this.Value & value) == value;
        public override string ToString() => this.Value.ToString("G");
    }
}
