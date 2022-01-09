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
using static System.Console;
using BLL.Services;

namespace Talker
{
    public class App
    {
        const string REGISTER = "register",
            LOGIN = "logIn",
            LOGOUT = "logOut",
            CHANGE_PARAMETERS_FOR_LOGIN = "cPar";

        private readonly IUserService _userService;
        private readonly PasswordValidationParameters _passwordValidationParameters;

        public App(IUserService userService, IOptions<PasswordValidationParameters> passwordValidationParameters)
        {
            _userService = userService;
            _passwordValidationParameters = passwordValidationParameters?.Value ?? throw new ArgumentNullException(nameof(passwordValidationParameters));
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

            _userService.Create(user);

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

            _userService.Create(newUser);
        }

        void Login(HashHandler hasher)
        {
            WriteLine("Enter username: ");
            string username = ReadLine();
            WriteLine("Enter password: ");
            string password = hasher.GetHash(ReadLine());

            try
            {
                Task<IEnumerable<User>> task = _userService.Read();
                IEnumerable<User> list = task.Result;

                if (list != null)
                {
                    foreach (var u in list)
                    {
                        if (u.Username == username && u.Password == password)
                        {
                            WriteLine("Logged in!");
                            ActionsWithLoggedUser(hasher, u);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLine($"An error occured: '{ex.Message}'");
            }
        }

        void ActionsWithLoggedUser(HashHandler hasher, User user)
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
                        itsNoLogOut = false;
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
            var newUser = new User();
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
                    WriteLine("New username cannot be same with old");
                }
            } while (isNotValidUsername);
            newUser.Username = newUsername;

            GetPassword(hasher, newUser);

            WriteLine(_userService.TryUpdate(user, newUser).Content);
        }

        void GetPassword(HashHandler hasher, User newUser)
        {
            WriteLine("Write new password:");
            var passwordValidatorInstance = new PasswordValidator();
            string newPassword = ReadLine();
            string passwordCorrection;
            do
            {
                passwordCorrection = passwordValidatorInstance.IsItValidPassword(newPassword, _passwordValidationParameters);
                if (!String.IsNullOrWhiteSpace(passwordCorrection))
                {
                    WriteLine(passwordCorrection);
                    newPassword = ReadLine();
                }
            } while (!String.IsNullOrWhiteSpace(passwordCorrection));

            newUser.Password = hasher.GetHash(newPassword);
        }
    }
}