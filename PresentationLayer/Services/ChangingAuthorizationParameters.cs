using BLL.Abstractions.Interfaces;
using BLL.Abstractions.Interfaces.Validators;
using Core.Models;
using PresentationLayer.Abstractions.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    class ChangingAuthorizationParameters : PLServiceWithPasswordValidations
    {
        private readonly ICrudService<User> _crudService;
        private readonly IHashHandler _hashHandler;
        private readonly IPasswordValidator _passwordValidator;
        private readonly Session _openedSession;

        public ChangingAuthorizationParameters(ICrudService<User> crudService, IHashHandler hashHandler, IPasswordValidator passwordValidator, Session openSession)
        {
            _crudService = crudService;
            _hashHandler = hashHandler;
            _passwordValidator = passwordValidator;
            _openedSession = openSession;
        }

        public override async Task Execute()
        {
            User tempUser = new User(_openedSession.LoggedUser);
            ChangeUsername(tempUser);

            GetPassword(tempUser, _hashHandler, _passwordValidator);

            bool succes;
            do
            {
                succes = await _crudService.TryUpdate(_openedSession.LoggedUser);
                if (succes)
                {
                    Console.WriteLine("Succesfully updated");
                }
                else
                {
                    Console.WriteLine("User with this name already exist");
                    ChangeUsername(tempUser);
                }
            } while (!succes);
            _openedSession.LoggedUser = tempUser;
        }

        void ChangeUsername(User user)
        {
            string newUsername;
            bool isNotValidUsername = true;
            do
            {
                Console.WriteLine("Write new user name:");
                newUsername = Console.ReadLine();
                if (!newUsername.Equals(user.Username))
                {
                    isNotValidUsername = false;
                }
                else
                {
                    Console.WriteLine("You don't change anything!");
                }
            } while (isNotValidUsername);
            user.Username = newUsername;
        }
    }
}
