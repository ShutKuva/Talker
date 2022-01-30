using BLL.Abstractions.Interfaces;
using BLL.Abstractions.Interfaces.Validators;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services.Setters
{
    public class Setter
    {
        ICrudService<User> _crudService;
        IHashHandler _hashHandler;
        IPasswordValidator _passwordValidator;

        public Setter(ICrudService<User> crudService, IHashHandler hashHandler, IPasswordValidator passwordValidator)
        {
            _crudService = crudService;
            _hashHandler = hashHandler;
            _passwordValidator = passwordValidator;
        }

        public void SetPassword(User newUser)
        {
            Console.WriteLine("Write new password:");
            string newPassword = Console.ReadLine();
            string passwordCorrection;

            do
            {
                passwordCorrection = _passwordValidator.IsItValidPassword(newPassword);
                if (!string.IsNullOrWhiteSpace(passwordCorrection))
                {
                    Console.WriteLine(passwordCorrection);
                    newPassword = Console.ReadLine();
                }
            } while (!string.IsNullOrWhiteSpace(passwordCorrection));

            newUser.Password = _hashHandler.GetHash(newPassword);
        }

        public void ChangeUsername(User user)
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

        public async Task SetUsername(User user)
        {
            ChangeUsername(user);
            IEnumerable<User> users;
            do
            {
                users = await _crudService.ReadWithCondition(temp => user.Username == temp.Username);
                if (users is not null)
                {
                    Console.WriteLine("User with this name already exist");
                    ChangeUsername(user);
                }
            } while (users is not null);
        }

        public async Task UpdateUser(User user)
        {
            bool updateSuccess = await _crudService.TryUpdate(user);
            if (updateSuccess)
            {
                Console.WriteLine("Succesfully updated");
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }
        }
    }
}
