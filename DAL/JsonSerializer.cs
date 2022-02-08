using System.IO;
using System.Threading.Tasks;
using DAL.Abstractions.Interfaces;
using Newtonsoft.Json;

namespace DAL
{
    public class JsonSerializer : ISerializer
    {
        public JsonSerializer()
        {
        }

        public async Task SaveToFileAsync<T>(T obj, string fileName)
        {
            var json = JsonConvert.SerializeObject(obj);
            await File.WriteAllTextAsync(fileName,json);
        }

        public async Task<T> LoadFromFileAsync<T>(string fileName)
        {
            var json = await File.ReadAllTextAsync(fileName);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}