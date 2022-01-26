using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RoomRoleJointService : IRoomRoleJointService
    {
        private readonly Room _room;
        private readonly IGenericRepository<RoomRoleJoint> _roomRoleJointRepository;

        public RoomRoleJointService(Room room, IGenericRepository<RoomRoleJoint> roomRoleJointRepository)
        {
            _room = room;
            _roomRoleJointRepository = roomRoleJointRepository;
        }

        public async Task<bool> CreateNewRole(CustomRole customRole)
        {
            if (customRole != null && customRole.RoleName != null)
            {
                RoomRoleJoint roomRoleJoint = new RoomRoleJoint(_room.Id, customRole.RoleName, (int)customRole.CurrentRoleRights);
                await _roomRoleJointRepository.CreateAsync(roomRoleJoint);

                return true;
            }

            return false;
        }

        public async Task<bool> ModifyRole(int customRoleId, string newRoleName, CustomRole.RoleRights? newRoleRights)
        {
            RoomRoleJoint customRole = await this.GetRole(customRoleId);

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

        public async Task<bool> DeleteRole(int customRoleId)
        {
            RoomRoleJoint customRole = await this.GetRole(customRoleId);

            if (customRole != null)
            {
                await _roomRoleJointRepository.DeleteAsync(customRole);

                return true;
            }

            return false;
        }

        public async Task<RoomRoleJoint> GetRole(int customRoleId)
        {
            RoomRoleJoint customRole = (await _roomRoleJointRepository.FindByConditionAsync(x => x._roomId == _room.Id && x._roleId == customRoleId)).FirstOrDefault();

            if (customRole != null)
            {
                return customRole;
            }

            return null;
        }
    }
}
