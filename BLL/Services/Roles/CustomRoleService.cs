using BLL.Abstractions.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Roles
{
    public class CustomRoleService : ICustomRoleService
    {
        private readonly BitOperationsHelper _bitOperationsHelper;
        private readonly Room _room;

        public CustomRoleService(BitOperationsHelper bitOperationsHelper, Room room)
        {
            _bitOperationsHelper = bitOperationsHelper;
            _room = room;
        }

        public async Task CreateNewCustomRole(string roleName, int roleRights)
        {
            if (roleRights >= 0 && roleRights < 32)
            {
                _bitOperationsHelper.Add((CustomRole.RoleRights)roleRights);
                CustomRole customRole = new CustomRole(roleName, _bitOperationsHelper.Value);

                await 
            }
        }

        public void DeleteCustomRole(int )

        public CustomRole ChangeCustomRole(CustomRole currentCustomRole, string roleName, int roleRights)
        {
            if (roleName != null)
            {
                currentCustomRole.SetRoleName(roleName);
            }
        }
    }
}
