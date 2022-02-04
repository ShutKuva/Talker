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
        private readonly IRoomUserJointService _roomUserJointService;

        public RoomRoleJointService(IGenericRepository<RoomRoleJoint> roomRoleJointRepository, IRoomUserJointService roomUserJointService)
        {
            _roomRoleJointRepository = roomRoleJointRepository;
            _roomUserJointService = roomUserJointService;
        }

        public async Task<bool> CreateNewRole(CustomRole customRole)
        {
            if (customRole != null && customRole.RoleName != null)
            {
                RoomRoleJoint roomRoleJoint = new RoomRoleJoint(customRole.RoleName, (int) customRole.CurrentRoleRights);
                await _roomRoleJointRepository.CreateAsync(roomRoleJoint);

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
                    customRole.RoleName = newRoleName;
                }

                if (newRoleRights != null)
                {
                    customRole.RoleRights = (int) newRoleRights;
                }

                await _roomRoleJointRepository.UpdateAsync(customRole);
                
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteRole(int customRoleId)
        {
            var customRole = await this.GetRoomRoleJoint(customRoleId);

            if (customRole != null)
            {
                await _roomRoleJointRepository.DeleteAsync(customRole);

                return true;
            }

            return false;
        }

        public async Task<RoomRoleJoint> GetRoomRoleJoint(int roomRoleId)
        {
            var roomRoleJoint = await this.ReadWithCondition(x => x.Id == roomRoleId);
            var roomRole = roomRoleJoint.FirstOrDefault();

            return roomRole;
        }

        public async Task<RoomRoleJoint> GetRoomRoleJoint(CustomRole customRole)
        {
            var roomRoleJoint = await this.ReadWithCondition(
                
                x => x.RoleName == customRole.RoleName &&
                x.RoleRights == (int) customRole.CurrentRoleRights
            );
            var roomRole = roomRoleJoint.FirstOrDefault();

            return roomRole;
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
