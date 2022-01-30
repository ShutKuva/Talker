using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Services
{
    class UserService : IUserService
    {
        private readonly IGenericRepository<User> _entityRepository;

        public UserService(IGenericRepository<User> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task<IEnumerable<User>> ReadWithCondition(Expression<Func<User, bool>> expression) // добавлен доп метод для удобства
        {
            IEnumerable<User> data = (await _entityRepository.FindByConditionAsync(expression)).ToList();

            return data;
        }

        public async Task<User> ReadUserWithCondition(Expression<Func<User, bool>> expression)
        {
            IEnumerable<User> data = await ReadWithCondition(expression);

            return data.FirstOrDefault();
        }
    }
}
