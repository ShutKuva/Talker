using System.Collections.Generic;
using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;
using System.Threading.Tasks;
using System.Linq;

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

        public async Task<List<User>> Read()
        {
            var allData = (await _userRepository.FindAllAsync()).ToList();
            return allData;
        }
    }
}