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
    class RoomService : IRoomService
    {
        private readonly IGenericRepository<Room> _roomRepository;

        public RoomService(IGenericRepository<Room> roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public void Create(Room room)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Room>> Read()
        {
            throw new NotImplementedException();
        }

        public void Update(Room room)
        {
            throw new NotImplementedException();
        }
    }
}
