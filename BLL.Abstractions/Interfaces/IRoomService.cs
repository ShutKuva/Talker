using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface IRoomService
    {
        void Create(Room room);
        void Delete(int id);
        void Update(Room room);
        Task<List<Room>> Read();
    }
}
