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
    public class RoomRoleJointService : IRoomRoleJointService
    {
        private readonly IGenericRepository<RoomRoleJoint> _roomRoleJointRepository;

        public RoomRoleJointService(IGenericRepository<RoomRoleJoint> roomRoleJointRepository)
        {
            _roomRoleJointRepository = roomRoleJointRepository;
        }

        public async Task<bool> CreateNewRole(Room room, CustomRole customRole)
        {
            if (customRole != null && customRole.RoleName != null)
            {
                RoomRoleJoint roomRoleJoint = new RoomRoleJoint(room.Id, customRole.RoleName, (int)customRole.CurrentRoleRights);
                await _roomRoleJointRepository.CreateAsync(roomRoleJoint);

                return true;
            }

            return false;
        }

        public async Task<bool> ModifyRole(Room room, int customRoleId, string newRoleName = null, CustomRole.RoleRights? newRoleRights = null)
        {
            var customRoles = await this.ReadWithCondition(x => x.Id == customRoleId);

            var customRole = customRoles.FirstOrDefault();

            if (customRole != null)
            {
                if (newRoleName != null)
                {
                    customRole._roleName = newRoleName;
                }

                if (newRoleRights != null)
                {
                    customRole._roleRights = (int)newRoleRights;
                }

                await _roomRoleJointRepository.UpdateAsync(customRole);
                
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteRole(Room room, int customRoleId)
        {
            RoomRoleJoint customRole = await this.GetRole(room, customRoleId);

            if (customRole != null)
            {
                await _roomRoleJointRepository.DeleteAsync(customRole);

                return true;
            }

            return false;
        }

        public async Task<IEnumerable<RoomRoleJoint>> ReadWithCondition(Expression<Func<RoomRoleJoint, bool>> expression) // добавлен доп метод для удобства
        {
            var data = await _roomRoleJointRepository.FindByConditionAsync(expression);

            if (data.Any())
            {
                return data.ToList();
            }

            return null;
        }
    }
}
