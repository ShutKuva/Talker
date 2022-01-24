using BLL.Abstractions.Interfaces;
using Core.Models;
using Core;
using static System.Console;
using BLL.Services;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using BLL.Validators;
using Microsoft.Extensions.Options;
using BLL.Abstractions.Interfaces.Validators;

namespace Talker
{
    public class App
    {
        const string REGISTER = "reg",
            LOGIN = "logIn",
            LOGOUT = "logOut",
            CHANGE_PARAMETERS_FOR_LOGIN = "cPar";

        private readonly ICrudService<User> _crudService;
        private readonly IUserService _userService;
        private readonly IPasswordValidator _passwordValidator;

        public App(ICrudService<User> crudService, IUserService userService, IPasswordValidator passwordValidator)
        {
            _userService = userService;
            _crudService = crudService;
            _passwordValidator = passwordValidator;
        }
        
        public void StartApp()
        {
            var hasher = new HashHandler();
            var user = new User();
            user.Id = 10;
            user.Name = "Pavel";
            user.Surname = "Petrenko";
            user.Username = "Pavlushko";
            user.Age = 63;
            user.Password = hasher.GetHash("12345");

            _crudService.Create(user);

            string command;

            while (true)
            {
                command = ReadLine();
                switch (command)
                {
                    case LOGIN:
                        Login(hasher);
                        break;
                    case REGISTER:
                        Register(hasher);
                        break;
                    default:
                        WriteLine("Unknown command");
                        break;
                }
            }
        }

        void Register(HashHandler hasher)
        {
            var newUser = new User();
            WriteLine("Write your name:");
            newUser.Name = ReadLine();
            WriteLine("Write your surname:");
            newUser.Surname = ReadLine();
            int age;
            bool ageFlag = true;
            do {
                WriteLine("Write your age:");
                if (!Int32.TryParse(ReadLine(), out age))
                {
                    WriteLine("Your age is no number");
                } else
                {
                    ageFlag = false;
                }
            } while (ageFlag);
            newUser.Age = age;
            WriteLine("Write your username:");
            newUser.Username = ReadLine();
            GetPassword(hasher, newUser);
            var random = new Random();
            newUser.Id = random.Next(10000, 99999);

            _crudService.Create(newUser);
        }

        void Login(HashHandler hasher)
        {
            WriteLine("Enter username: ");
            string username = ReadLine();
            WriteLine("Enter password: ");
            string password = hasher.GetHash(ReadLine());

            User loginUser = new User()
            {
                Username = username,
                Password = password
            };

            try
            {
                Task<User> task = _userService.ReadUserWithCondition(user => user.Username == loginUser.Username && user.Password == loginUser.Password);
                User u = task.Result;

                if (u == null)
                {
                    WriteLine("User doesn't exist!");

                    return;
                }

                WriteLine("Logged in!");
                DoActionsWithLoggedUser(hasher, u);
            }
            catch (Exception ex)
            {
                WriteLine($"An error occured: '{ex.Message}'");
            }
        }

        void DoActionsWithLoggedUser(HashHandler hasher, User user) 
        {
            string command;
            bool itsNoLogOut = true;

            while (itsNoLogOut)
            {
                command = ReadLine();
                switch (command)
                {
                    case CHANGE_PARAMETERS_FOR_LOGIN:
                        ChangeLoginParameters(hasher, user);
                        break;
                    case LOGOUT:
                        WriteLine("Good bye!");
                        itsNoLogOut = false;
                        break;
                }
            }
        }

        void ChangeLoginParameters(HashHandler hasher, User user)
        {
            string newUsername;
            bool isNotValidUsername = true;

            do
            {
                WriteLine("Write new user name:");
                newUsername = ReadLine();
                if (!newUsername.Equals(user.Username))
                {
                    isNotValidUsername = false;
                } else
                {
                    WriteLine("New username has no differences with the old one!");
                }
            } while (isNotValidUsername);

            user.Username = newUsername;

            GetPassword(hasher, user);

            if (_crudService.TryUpdate(user).Result) {
                WriteLine("Succesfully updated");
            } else
            {
                WriteLine("User with this name already exist");
            }
        }

        void GetPassword(HashHandler hasher, User newUser)
        {
            WriteLine("Write new password:");
            string newPassword = ReadLine();
            string passwordCorrection;

            do
            {
                passwordCorrection = _passwordValidator.IsItValidPassword(newPassword);
                if (!string.IsNullOrWhiteSpace(passwordCorrection))
                {
                    WriteLine(passwordCorrection);
                    newPassword = ReadLine();
                }
            } while (!string.IsNullOrWhiteSpace(passwordCorrection));

            newUser.Password = hasher.GetHash(newPassword);
        }
    }
}