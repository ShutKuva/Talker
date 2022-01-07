using BLL.Abstractions.Interfaces;
using Core.Models;
using static System.Console;
using BLL.Services;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using PresentationLayer.Validators;

namespace ConsoleApp3
{
    public class App
    {
        const string REGISTER = "register",
            LOGIN = "logIn",
            LOGOUT = "logOut",
            CHANGE_PARAMETERS_FORLOGIN = "cPar";

        private readonly IUserService _userService;

        public App(IUserService userService)
        {
            _userService = userService;
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

            string command = ReadLine();

            while (true)
            {
                switch(command)
                {
                    case LOGIN:
                        Login(hasher);
                        break;
                    default:
                        WriteLine("Unknown command");
                        break;
                }
            }
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
            string command = ReadLine();
            bool itsNoLogOut = true;

            while (itsNoLogOut)
            {
                switch (command)
                {
                    case CHANGE_PARAMETERS_FORLOGIN:
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
            WriteLine("Write new password:");
            var passwordValidatorInstance = new PasswordValidator();
            string newPassword = ReadLine();
            string passwordCorrection;
            do
            {
                passwordCorrection = passwordValidatorInstance.IsItValidPassword(newPassword);
                if (!String.IsNullOrWhiteSpace(passwordCorrection))
                {
                    WriteLine(passwordCorrection);
                    newPassword = ReadLine();
                }
            } while (!String.IsNullOrWhiteSpace(passwordCorrection));

            user.Password = hasher.GetHash(newPassword);

            _userService.Update(user, newUser);
        }
    }
}