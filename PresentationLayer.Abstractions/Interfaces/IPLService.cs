using System;
using System.Threading.Tasks;

namespace PresentationLayer.Abstractions.Interfaces
{
    public interface IPLService
    {
        public Task Execute(string[] command);
    }
}
