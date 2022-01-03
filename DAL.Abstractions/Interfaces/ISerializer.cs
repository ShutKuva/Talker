using System.Threading.Tasks;
using Core.Models;

namespace DAL.Abstractions.Interfaces
{
    public interface ISerializer
    {
        Task SaveToFileAsync<T>(T obj, string fileName);
        
        Task<T> LoadFromFileAsync<T>(string fileName);
    }
}