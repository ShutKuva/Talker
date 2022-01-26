using BLL.Abstractions.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    class UserService : IUserService
    {
        ICrudService<User> 
        public Task<User> ReadUserWithCondition(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> ReadWithCondition(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
