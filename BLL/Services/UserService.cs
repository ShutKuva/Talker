using System.Collections.Generic;
using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Create(User user)
        {
            _userRepository.CreateAsync(user);
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async void Update(User user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public IEnumerable<User> Get()
        {
            throw new System.NotImplementedException();
        }
    }
}