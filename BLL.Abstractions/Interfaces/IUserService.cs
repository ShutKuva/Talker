using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> ReadWithCondition(Expression<Func<User, bool>> expression);

        public Task<User> ReadUserWithCondition(Expression<Func<User, bool>> expression);
    }
}
