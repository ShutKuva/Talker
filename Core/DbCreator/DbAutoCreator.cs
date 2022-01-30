using System.Reflection;
using System;
using System.IO;
using System.Linq;

namespace Core.DbCreator
{
    public class DbAutoCreator : IDbAutoCreator
    {
        [AttributeUsage(AttributeTargets.Class)]
        public class AutoDBAttribute : Attribute { }

        private readonly string _dbDirectory;

        public DbAutoCreator(string dbDirectory)
        {
            _dbDirectory = dbDirectory;
        }

        public void GenerateDb()
        {
            var assembly = Assembly.Load(AssemblyName.GetAssemblyName(@"Core.dll"));

            if (!Directory.Exists(_dbDirectory))
            {
                Directory.CreateDirectory(_dbDirectory);
            }

            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute<AutoDBAttribute>();

                var directoryInfo = new DirectoryInfo(_dbDirectory);

                if (attribute != null && directoryInfo.GetFiles().Where((x) =>
                x.Name.Contains(type.Name)).FirstOrDefault() == null)
                {
                    var path = Path.Combine(_dbDirectory, type.Name + "Db.json");
                    File.Create(path);
                    File.WriteAllText("[]", path);
                }
            }
        }
    }
}
