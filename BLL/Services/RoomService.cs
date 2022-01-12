using BLL.Abstractions.Interfaces;
using Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    class RoomService : IRoomService
    {
        private readonly ICrudService<Room> _roomCrudService;

        public RoomService(ICrudService<Room> roomCrudService)
        {
            _roomCrudService = roomCrudService;
        }

        public async Task<bool> GetStatusForUser(Room room, User user, Room.Status status)
        {
            var temp = _roomCrudService.Read(room.Id).Result;

            if (temp == null)
            {
                return false;
            }

            var pair = room.UserStatusPairs.Where((x) => x.Key.Id == user.Id).FirstOrDefault();
            if (pair.Key == null)
            {
                return false;
            }
            room.UserStatusPairs[user] = status;
            await _roomCrudService.TryUpdate(room);

            return true;
        }
    }
}
