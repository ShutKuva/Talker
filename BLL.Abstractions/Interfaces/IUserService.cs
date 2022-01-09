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
        Task<IEnumerable<User>> Read(User user);

        //CustomResult TryUpdate(User user, User newUser); // этот метод не должен быть в интерфейсе
    }
}