using BLL.Abstractions.Interfaces;
using Core.Models;
using PresentationLayer.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    public class Login : IPLService
    {
        private readonly IHashHandler _hashHandler;
        private readonly ICrudService<User> _crudService;
        private readonly Session _openedSession;

        public Login(IHashHandler hashHandler, ICrudService<User> crudService, Session openedSession)
        {
            _hashHandler = hashHandler;
            _crudService = crudService;
            _openedSession = openedSession;
        }

        public async Task Execute(string[] command)
        {
            Console.WriteLine("Enter username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            string password = _hashHandler.GetHash(Console.ReadLine());

            User loginUser = new User()
            {
                Username = username,
                Password = password
            };

            var u = await _crudService.ReadWithCondition(
                user => user.Username == loginUser.Username && user.Password == loginUser.Password
            );

            if (u == null)
            {
                Console.WriteLine("User doesn't exist! Maybe wrong username or password.");
            } else
            {
                _openedSession.LoggedUser = u.FirstOrDefault();
                _openedSession.MyLocation = Location.Main;
                Console.WriteLine("Logged in!");
            }
        }
    }
}
