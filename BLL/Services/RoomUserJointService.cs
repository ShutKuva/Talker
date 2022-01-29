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
    public class RoomUserJointService : IRoomUserJointService
    {
        private readonly IGenericRepository<RoomUserJoint> _roomUserJointRepository;
    }
}
