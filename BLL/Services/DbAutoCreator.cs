using System.Reflection;
using System;
using System.IO;
using System.Linq;
using BLL.Abstractions.Interfaces;

namespace BLL.Services
{
    public class DbAutoCreator: IDbAutoCreator
    {
        [AttributeUsage(AttributeTargets.Class)]
        public class AutoDBAttribute: Attribute { }

        private readonly string _objWarehousePath;
        private readonly string _dbDirectory;

        public DbAutoCreator(string objWarehousePath, string dbDirectory)
        {
            _objWarehousePath = objWarehousePath;
            _dbDirectory = dbDirectory;
        }

        public void GenerateDb()
        {
            var assembly = Assembly.LoadFrom(_objWarehousePath);

            if (!Directory.Exists(_dbDirectory))
            {
                Directory.CreateDirectory(_dbDirectory);
            }

            foreach (var type in assembly.GetTypes())
            {
                var attribute = type.GetCustomAttribute<AutoDBAttribute>();

                var directoryInfo = new DirectoryInfo(_dbDirectory);

                if (attribute != null && directoryInfo.GetFiles().Where((x) =>
                x.Name.Contains(type.GetType().Name)).FirstOrDefault() == null)
                {
                    File.Create(Path.Combine(_dbDirectory, type.GetType().Name + "Db.json"));
                }
            }
        }
    }
}
