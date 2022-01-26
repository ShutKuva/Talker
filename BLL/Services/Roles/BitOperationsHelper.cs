using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Models.CustomRole;

namespace BLL.Services.Roles
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
