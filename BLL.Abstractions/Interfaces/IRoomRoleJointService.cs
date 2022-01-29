using Core.Models;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface IRoomRoleJointService
    {
        Task<bool> CreateNewRole(CustomRole customRole);

        Task<bool> ModifyRole(int customRoleId, string newRoleName = null, CustomRole.RoleRights? newRoleRights = null);

        Task<bool> DeleteRole(int customRoleId);

        Task<RoomRoleJoint> GetRole(int customRoleId);
    }
}
