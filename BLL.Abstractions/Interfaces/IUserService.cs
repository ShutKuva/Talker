using System.Collections.Generic;
using Core.Models;

namespace BLL.Abstractions.Interfaces
{
    public interface IUserService
    {
        void Create(User user);
        void Delete(int id);
        void Update(User user);
        IEnumerable<User> Read();
    }
}