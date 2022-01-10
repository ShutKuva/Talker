using System.Collections.Generic;
using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Create(User user)
        {
            var u = Read(user);

            if (u != null)
            {
                return false;
            }
            _userRepository.CreateAsync(user);

            return true;
        }

        public bool Delete(User user)
        {
            var u = Read(user).Result;

            if (u == null)
            {
                return false;
            }
            _userRepository.DeleteAsync(user);

            return true;
        }

        public CustomResult TryUpdate(User user, User newUser) // public method for updating user
        {
            var usersWithSameUsername = ReadWithCondition((x) => x.Username == newUser.Username).Result;

            if (usersWithSameUsername != null)
            {
                return new CustomResult() { Content = "Already used username" };
            }

            user.Username = newUser.Username;
            user.Password = newUser.Password;
            _userRepository.UpdateAsync(user);

            return new CustomResult() { Content = "Succesfully updated" };
        }

        public async Task<User> Read(User user)
        {

            var u = (await _userRepository.FindAllAsync()).ToList().FirstOrDefault((x) =>
           x.Username == user.Username &&
           x.Password == user.Password);

            return u;
        }

        public async Task<IEnumerable<User>> ReadWithCondition(Expression<Func<User, bool>> expression) // добавлен доп метод для удобства
        {
            var data = (await _userRepository.FindByConditionAsync(expression)).ToList();

            return data;
        }
    }
}