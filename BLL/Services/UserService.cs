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

        public void Create(User user)
        {
            _userRepository.CreateAsync(user);
        }

        public void Delete(User user)
        {
            _userRepository.DeleteAsync(user);
        }

        public void Update(User user)
        {
            _userRepository.UpdateAsync(user);
        }

        public CustomResult TryUpdate(User user, User newUser) // переделан под новый метод Read(User user)
        {
            var usersWithSameUsername = ReadWithCondition((x) => x.Username == newUser.Username).Result;

            if (usersWithSameUsername != null)
            {
                return new CustomResult() { Content = "Already used username" }; ;
            }

            user.Username = newUser.Username;
            user.Password = newUser.Password;
            Update(user);

            return new CustomResult() { Content = "Succesfully updated" };
        }

        public async Task<User> Read(User user) // here returns user
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