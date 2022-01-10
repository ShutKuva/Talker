using Core.Models;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface IUserService
    {
        bool Create(User user);

        bool Delete(User user);

        Task<User> Read(User user);

        CustomResult TryUpdate(User user, User newUser);
    }
}