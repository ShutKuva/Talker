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
using BLL.Exceptions;
using BLL.GuardClauses;

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
            var guard = new Guard();
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
                        Login(hasher, guard);
                        break;
                    case REGISTER:
                        Register(hasher, guard);
                        break;
                    default:
                        WriteLine("Unknown command");
                        break;
                }
            }
        }

        void Register(HashHandler hasher, Guard guard)
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
                try
                {
                    guard.CheckStrict(new WrongInputDataException("Your age is no number"), Int32.TryParse(ReadLine(), out age));
                    newUser.Age = age;
                    ageFlag = false;
                }
                catch (WrongInputDataException ex)
                {
                    WriteLine(ex.Message);
                }
            } while (ageFlag);

            WriteLine("Write your username:");
            newUser.Username = ReadLine();
            GetPassword(hasher, newUser, guard);
            var random = new Random();
            newUser.Id = random.Next(10000, 99999);

            _crudService.Create(newUser);
        }

        void Login(HashHandler hasher, Guard guard)
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
                User user = task.Result;

                guard.CheckStrict(new WrongInputDataException("User doesn't exist!"), user is not null);

                WriteLine("Logged in!");
                DoActionsWithLoggedUser(hasher, user, guard);
            }
            catch (WrongInputDataException ex)
            {
                WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                WriteLine($"An error occured: '{ex.Message}'");
            }
        }

        void DoActionsWithLoggedUser(HashHandler hasher, User user, Guard guard) 
        {
            string command;
            bool itsNoLogOut = true;

            while (itsNoLogOut)
            {
                command = ReadLine();
                switch (command)
                {
                    case CHANGE_PARAMETERS_FOR_LOGIN:
                        ChangeLoginParameters(hasher, user, guard);
                        itsNoLogOut = false;
                        break;
                    case LOGOUT:
                        WriteLine("Good bye!");
                        itsNoLogOut = false;
                        break;
                }
            }
        }

        void ChangeLoginParameters(HashHandler hasher, User user, Guard guard)
        {
            string newUsername;
            bool isNotValidUsername = true;

            do
            {
                WriteLine("Write new user name:");
                newUsername = ReadLine();
                try
                {
                    guard.CheckStrict(new WrongInputDataException("New username has no differences with the old one!"), !newUsername.Equals(user.Username));

                    user.Username = newUsername;

                    GetPassword(hasher, user, guard);

                    guard.CheckStrict(new WrongInputDataException("User with this name already exist"), _crudService.TryUpdate(user).Result);

                    WriteLine("Succesfully updated");
                    isNotValidUsername = false;
                }
                catch (WrongInputDataException ex)
                {
                    WriteLine(ex.Message);
                }
            } while (isNotValidUsername);
        }

        void GetPassword(HashHandler hasher, User newUser, Guard guard)
        {
            WriteLine("Write new password:");
            string newPassword;
            string passwordCorrection;

            do
            {
                newPassword = ReadLine();
                passwordCorrection = _passwordValidator.IsItValidPassword(newPassword);
                try
                {
                    guard.CheckStrict(new WrongInputDataException(passwordCorrection), string.IsNullOrWhiteSpace(passwordCorrection));
                }
                catch (WrongInputDataException ex)
                {
                    WriteLine(ex.Message);
                }
            } while (!string.IsNullOrWhiteSpace(passwordCorrection));

            newUser.Password = hasher.GetHash(newPassword);
        }
    }
}