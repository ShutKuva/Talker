using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface IRoomService
    {
        public Task<bool> GetStatusForUser(Room room, User user, Room.Status status);
    }
}
