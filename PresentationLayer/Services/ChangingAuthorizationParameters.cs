using BLL.Abstractions.Interfaces;
using BLL.Abstractions.Interfaces.Validators;
using Core.Models;
using PresentationLayer.Abstractions.Interfaces;
using PresentationLayer.Services.Setters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    class ChangingAuthorizationParameters : IPLService
    {
        private const string PASSWORD = "-p",
            USERNAME = "-u";
        private readonly Session _openedSession;
        private readonly Setter _setter;

        public ChangingAuthorizationParameters(Setter setter, Session openSession)
        {
            _setter = setter;
            _openedSession = openSession;
        }

        public async Task Execute(string[] command)
        {
            User tempUser = new User(_openedSession.LoggedUser);

            if (command.Length == 1)
            {
                await _setter.SetUsername(tempUser);
                _setter.SetPassword(tempUser);
            } else
            {
                if (command.Contains(USERNAME))
                {
                    await _setter.SetUsername(tempUser);
                }
                if (command.Contains(PASSWORD))
                {
                    _setter.SetPassword(tempUser);
                }
            }
            await _setter.UpdateUser(tempUser);
        }
    }
}
