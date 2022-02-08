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

namespace PesentationLayer.Services
{
    class RegisterNewUser : IPLService
    {
        private readonly ICrudService<User> _crudService;
        private readonly Setter _setter;

        public RegisterNewUser(ICrudService<User> crudService, Setter setter)
        {
            _crudService = crudService;
            _setter = setter;
        }

        public async Task Execute(string[] command)
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
            _setter.SetPassword(newUser);

            bool success;

            do
            {
                success = await _crudService.Create(newUser, dbUser => newUser.Username == dbUser.Username);
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
