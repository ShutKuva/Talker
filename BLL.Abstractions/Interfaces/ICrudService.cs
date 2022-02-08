using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface ICrudService<T> where T : BaseEntity
    {
        Task<bool> Create(T entity, Expression<Func<T, bool>> predicate = null);

        Task<bool> Delete(int id);

        Task<IEnumerable<T>> ReadWithCondition(Expression<Func<T, bool>> condition);

        Task<bool> TryUpdate(T entity);
    }
}
