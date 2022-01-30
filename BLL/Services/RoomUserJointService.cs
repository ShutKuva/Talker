using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RoomUserJointService : IRoomUserJointService
    {
        private readonly IGenericRepository<RoomUserJoint> _roomUserJointRepository;

        public RoomUserJointService(IGenericRepository<RoomUserJoint> roomUserJointRepository)
        {
            _roomUserJointRepository = roomUserJointRepository;
        }

        public async Task<IEnumerable<RoomUserJoint>> ReadWithCondition(Expression<Func<RoomUserJoint, bool>> expression) // добавлен доп метод для удобства
        {
            IEnumerable<RoomUserJoint> data = (await _roomUserJointRepository.FindByConditionAsync(expression)).ToList();

            return data;
        }
    }
}
