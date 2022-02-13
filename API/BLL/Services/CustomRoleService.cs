using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CustomRoleService : ICustomRoleService
    {
        private readonly IGenericRepository<CustomRole> _customRoleRepository;

        public CustomRoleService(IGenericRepository<CustomRole> customRoleRepository)
        {
            _customRoleRepository = customRoleRepository;
        }

        public async Task<bool> CreateNewRole(CustomRole customRole)
        {
            if (customRole != null && customRole.RoleName != null)
            {
                var role = new CustomRole(customRole.RoleName, (int) customRole.CurrentRoleRights);
                await _customRoleRepository.CreateAsync(role);

                return true;
            }

            return false;
        }

        public async Task<bool> ModifyRole(int customRoleId, string newRoleName = null, CustomRole.RoleRights? newRoleRights = null)
        {
            var customRoles = await this.ReadWithCondition(x => x.Id == customRoleId);

            var customRole = customRoles.FirstOrDefault();

            if (customRole != null)
            {
                if (newRoleName != null)
                {
                    customRole.SetRoleName(newRoleName);
                }

                if (newRoleRights != null)
                {
                    customRole.SetRoleRights((int) newRoleRights);
                }

                await _customRoleRepository.UpdateAsync(customRole);
                
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteRole(int customRoleId)
        {
            var customRole = await this.GetRoomRoleJoint(customRoleId);

            if (customRole != null)
            {
                await _customRoleRepository.DeleteAsync(customRole);

                return true;
            }

            return false;
        }

        public async Task<CustomRole> GetRoomRoleJoint(int roomRoleId)
        {
            var roomRoleJoint = await this.ReadWithCondition(x => x.Id == roomRoleId);
            var roomRole = roomRoleJoint.FirstOrDefault();

            return roomRole;
        }

        public async Task<IEnumerable<CustomRole>> ReadWithCondition(Expression<Func<CustomRole, bool>> expression) // добавлен доп метод для удобства
        {
            var data = await _customRoleRepository.FindByConditionAsync(expression);

            if (data.Any())
            {
                return data.ToList();
            }

            return null;
        }
    }
}
