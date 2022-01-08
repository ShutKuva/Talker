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

        public void Update(User user)
        {
            _userRepository.UpdateAsync(user);
        }

        public CustomResult TryUpdate(User user, User newUser)
        {
            Task<IEnumerable<User>> task = Read();
            IEnumerable<User> list = task.Result;
            User parent = user;
            bool itsValidUserName = true;
            foreach (User temp in list)
            {
                if (newUser.Username.Equals(temp.Username))
                {
                    itsValidUserName = false;
                    break;
                }
            }

            if (itsValidUserName)
            {
                user.Username = newUser.Username;
                user.Password = newUser.Password;
                Update(user);
                return new CustomResult() { Content = "Succesfully updated" };
            } 
            else
            {
                return new CustomResult() { Content = "Already used username" };
            }
        }

        public async Task<IEnumerable<User>> Read()
        {
            var allData = (await _userRepository.FindAllAsync()).ToList();
            return allData;
        }
    }
}