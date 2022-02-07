using BLL.Abstractions.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using BLL.Abstractions.Interfaces.Validators;
using PesentationLayer.Services;
using PresentationLayer.Abstractions.Interfaces;
using PresentationLayer.Services;
using System.Text;
using PresentationLayer.Services.Setters;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class App
    {
        private readonly Session _openedSession = new Session();
        private readonly Dictionary<Location, Dictionary<string, IPLService>> _allOperations;

        public App(ICrudService<User> crudUser,
                   ICrudService<Room> crudRoom,
                   ICrudService<CustomRole> crudRole,
                   ICrudService<Chat> crudChat,
                   ICrudService<Message> crudMessage,
                   ICrudService<RoomUserJoint> crudRoomUser,
                   IRoomUserJointService roomUserJointService,
                   IHashHandler hashHandler,
                   IPasswordValidator passwordValidator,
                   Setter setter)
        {
            _allOperations = new Dictionary<Location, Dictionary<string, IPLService>>
            {
                [Location.Unlogged] = new Dictionary<string, IPLService>
                {
                    ["reg"] = new RegisterNewUser(crudUser, setter),
                    ["logIn"] = new Login(hashHandler, crudUser, _openedSession)
                },
                [Location.Main] = new Dictionary<string, IPLService>
                {
                    ["cPar"] = new ChangingAuthorizationParameters(setter, _openedSession),
                    ["logOut"] = new Logout(_openedSession),
                    ["crRoom"] = new CreateNewRoom(crudRoom, crudUser, crudRole, roomUserJointService, _openedSession),
                    ["openRoom"] = new OpenRoom(_openedSession, crudRoomUser),
                    ["regInRoom"] = new RegisterInRoom(roomUserJointService, crudRoom, _openedSession)
                },
                [Location.InRoom] = new Dictionary<string, IPLService>
                {
                    ["crChat"] = new CreateNewChat(crudChat, _openedSession),
                    ["openChat"] = new OpenChat(crudChat, _openedSession)
                },
                [Location.InChat] = new Dictionary<string, IPLService>
                {
                    ["mes"] = new WriteMessage(crudChat, crudMessage, _openedSession)
                }
            };
        }
        
        public void StartApp()
        {
            string[] command;
            Dictionary<string, IPLService> dictionaryWithOperations;
            IPLService service;

            while (true)
            {
                _allOperations.TryGetValue(_openedSession.MyLocation, out dictionaryWithOperations);
                HelpCommands();
                command = Console.ReadLine().Split(" ");
                if (dictionaryWithOperations.TryGetValue(command[0], out service))
                {
                    try 
                    {
                        service.Execute(command);
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

        public void HelpCommands()
        {
            StringBuilder tempString = new StringBuilder();
            tempString.Append("Write command (you can use ");
            var tempDirectory = new Dictionary<string, IPLService>();
            if(_allOperations.TryGetValue(_openedSession.MyLocation, out tempDirectory))
            {
                int i = 0;
                foreach (string tempKey in tempDirectory.Keys)
                {
                    if (i == tempDirectory.Keys.Count - 1)
                    {
                        tempString.Append("\"" + tempKey + "\")");
                    } else
                    {
                        tempString.Append("\"" + tempKey + "\", ");
                    }
                    i++;
                }

                Console.WriteLine(tempString.ToString());
            } else
            {
                Console.WriteLine("Something went wrong");
            }
        }
    }
}