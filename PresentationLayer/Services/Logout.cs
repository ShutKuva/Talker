﻿using PresentationLayer.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    class Logout : IPLService
    {
        private readonly Session _openSession;

        public Logout(Session openSession)
        {
            _openSession = openSession;
        }

        public Task Execute(string[] command)
        {
            _openSession.PushBack();
            Console.WriteLine("Good bye!");
            return Task.CompletedTask;
        }
    }
}
