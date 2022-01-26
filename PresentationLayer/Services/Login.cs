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

        public Login(IHashHandler hashHandler, ICrudService<User> crudService, Session openSession)
        {
            _hashHandler = hashHandler;
            _crudService = crudService;
            _openedSession = openSession;
        }

        public async Task Execute()
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
            
            User u = (await _crudService.ReadWithCondition(user => user.Username == loginUser.Username && user.Password == loginUser.Password)).FirstOrDefault();

            if (u == null)
            {
                Console.WriteLine("User doesn't exist! Maybe wrong username or password.");
            } else
            {
                _openedSession.LoggedUser = u;
                _openedSession.MyLocation = Location.InMain;
                Console.WriteLine("Logged in!");
            }
        }
    }
}
