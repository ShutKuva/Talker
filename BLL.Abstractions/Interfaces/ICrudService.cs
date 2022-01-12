using Core.Models;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface ICrudService<T> where T : BaseEntity
    {
        Task<bool> Create(T entity);

        Task<bool> Delete(int id);

        Task<T> Read(int id);

        Task<bool> TryUpdate(T entity);
    }
}
