using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> ReadWithCondition(Expression<Func<User, bool>> expression);

        public Task<User> ReadUserWithCondition(Expression<Func<User, bool>> expression);
    }
}
