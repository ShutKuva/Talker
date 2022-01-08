using System.Collections.Generic;
using Core.Models;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface IUserService
    {
        void Create(User user);

        void Delete(int id);

        void Update(User user);

        CustomResult TryUpdate(User user, User newUser);

        Task<IEnumerable<User>> Read();
    }
}