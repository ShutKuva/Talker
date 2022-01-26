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
    public class CrudService<T> : ICrudService<T> where T : BaseEntity
    {
        private readonly IGenericRepository<T> _entityRepository;

        public CrudService(IGenericRepository<T> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task<bool> Create(T entity)
        {
            var u = Read(entity.Id).Result;

            if (u != null)
            {
                return false;
            }
            await _entityRepository.CreateAsync(entity);

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var u = Read(id).Result;

            if (u == null)
            {
                return false;
            }
            await _entityRepository.DeleteAsync(u);

            return true;
        }

        //public async Task<bool> TryUpdate(User user, User newUser) // public method for updating user
        //{
        //    var usersWithSameUsername = Read(user).Result;

        //    if (usersWithSameUsername != null)
        //    {
        //        return false;
        //    }

        //    newUser.Id = user.Id;

        //    await _userRepository.UpdateAsync(newUser);

        //    return true;
        //}

        public async Task<bool> TryUpdate(T entity) // public method for updating user
        {
            var usersWithSameUsername = Read(entity.Id).Result;

            if (usersWithSameUsername != null)
            {
                return false;
            }
            await _entityRepository.UpdateAsync(entity);

            return true;
        }

        public async Task<T> Read(int id)
        {
            var u = (await _entityRepository.FindAllAsync()).ToList().FirstOrDefault((x) => 
                x.Id == id);

            return u;
        }
        public async Task<IEnumerable<T>> ReadWithCondition(Expression<Func<T, bool>> expression) // добавлен доп метод для удобства
        {
            var data = (await _entityRepository.FindByConditionAsync(expression)).ToList();

            return data;
        }
    }
}