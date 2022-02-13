using Core;
using Core.Models;
using DAL.Abstractions.Interfaces;
using DAL.EFContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EFRepository
{
    class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly TalkerDbContext _dbContext;

        public GenericRepository(TalkerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
            Console.WriteLine(_dbContext.Entry(entity).State);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return Task.FromResult(_dbContext.Set<T>().Where(expression).AsEnumerable());
        }

        public async Task UpdateAsync(T entity)
        {
            var tempEntities = await FindByConditionAsync(temp => temp.Id == entity.Id);
            T entityInDb = tempEntities.FirstOrDefault();
            _dbContext.Update(entityInDb).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
