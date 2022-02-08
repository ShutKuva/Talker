using PresentationLayer.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    public class Navigation : IPLService
    {
        private readonly Session _openedSession;

        public Location GetLocation { get => _openedSession.MyLocation; }

        public Navigation(Session openedSession)
        {
            _openedSession = openedSession;
        }

        public Task Execute(string[] command)
        {
            _openedSession.PushBack();
            return Task.CompletedTask;
        }
    }
}
