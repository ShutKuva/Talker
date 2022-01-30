using BLL.Abstractions.Interfaces;
using Core.Models;
using Core;
using BLL.Services;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using BLL.Validators;
using Microsoft.Extensions.Options;
using BLL.Abstractions.Interfaces.Validators;
using PesentationLayer.Services;
using PresentationLayer.Abstractions.Interfaces;
using PresentationLayer.Services;

namespace PresentationLayer
{
    public class App
    {
        private readonly Session _openedSession = new Session();
        private readonly Dictionary<Location, Dictionary<string, IPLService>> _allOperations;

        public App(ICrudService<User> crudService, IHashHandler hashHandler, IPasswordValidator passwordValidator)
        {
            _allOperations = new Dictionary<Location, Dictionary<string, IPLService>>
            {
                [Location.Unlogged] = new Dictionary<string, IPLService>
                {
                    ["reg"] = new RegisterNewUser(crudService, hashHandler, passwordValidator),
                    ["logIn"] = new Login(hashHandler, crudService, _openedSession)
                },
                [Location.Main] = new Dictionary<string, IPLService>
                {
                    ["cPar"] = new ChangingAuthorizationParameters(crudService, hashHandler, passwordValidator, _openedSession),
                    ["logOut"] = new Logout(_openedSession)
                }
            };
        }
        
        public void StartApp()
        {
            string command;
            Dictionary<string, IPLService> dictionaryWithOperations;
            IPLService service;

            while (true)
            {
                _allOperations.TryGetValue(_openedSession.MyLocation, out dictionaryWithOperations);
                command = Console.ReadLine();
                if (dictionaryWithOperations.TryGetValue(command, out service))
                {
                    try 
                    {
                        service.Execute();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                } else
                {
                    Console.WriteLine("Unknown command");
                }
            }
        }
    }
}