using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface ICustomRoleService
    {
        CustomRole CreateNewCustomRole(string roleName, int roleRight);

        CustomRole ChangeCustomRole(CustomRole currentCustomRole, CustomRole.RoleRights[] roleRights);


    }
}
