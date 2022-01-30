using BLL.Abstractions.Interfaces;
using BLL.Abstractions.Interfaces.Validators;
using Core.Models;
using PresentationLayer.Abstractions.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PesentationLayer.Services
{
    class RegisterNewUser : PLServiceWithPasswordValidations
    {
        private readonly ICrudService<User> _crudService;
        private readonly IPasswordValidator _passwordValidator;
        private readonly IHashHandler _hashHandler;

        public RegisterNewUser(ICrudService<User> crudService, IHashHandler hashHandler, IPasswordValidator passwordValidator)
        {
            _crudService = crudService;
            _hashHandler = hashHandler;
            _passwordValidator = passwordValidator;
        }

        public override async Task Execute(string[] command)
        {
            var newUser = new User();
            Console.WriteLine("Write your name:");
            newUser.Name = Console.ReadLine();
            Console.WriteLine("Write your surname:");
            newUser.Surname = Console.ReadLine();
            int age;
            bool ageFlag = true;
            do
            {
                Console.WriteLine("Write your age:");
                if (!Int32.TryParse(Console.ReadLine(), out age))
                {
                    Console.WriteLine("Your age is no number");
                }
                else
                {
                    ageFlag = false;
                }
            }
            while (ageFlag);

            newUser.Age = age;
            Console.WriteLine("Write your username:");
            newUser.Username = Console.ReadLine();
            GetPassword(newUser, _hashHandler, _passwordValidator);
            var random = new Random();
            newUser.Id = random.Next(10000, 99999);

            bool success;

            do
            {
                success = await _crudService.Create(newUser);
                if (success)
                {
                    Console.WriteLine("Succesfully registered!");
                }
                else
                {
                    Console.WriteLine("User with same username already exists, try another:");
                    newUser.Username = Console.ReadLine();
                }
            } while (!success);
        }
    }
}
