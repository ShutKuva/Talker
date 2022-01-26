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
        private readonly RoomRoleJoint _roomRoleJoint;
        private readonly IGenericRepository<RoomRoleJoint> _roomRoleJointRepository;

        public RoomRoleJointService(RoomRoleJoint roomRoleJoint, IGenericRepository<RoomRoleJoint> roomRoleJointRepository)
        {
            _roomRoleJoint = roomRoleJoint;
            _roomRoleJointRepository = roomRoleJointRepository;
        }


    }
}
