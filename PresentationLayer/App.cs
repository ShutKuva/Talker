using BLL.Abstractions.Interfaces;
using Core.Models;
using static System.Console;
using BLL.Services;
using System;

namespace ConsoleApp3
{
    public class App
    {
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
                    case "logIn":
                        WriteLine("Enter username: ");
                        string username = ReadLine();
                        WriteLine("Enter password: ");
                        string password = hasher.GetHash(ReadLine());

                        try
                        {
                            var task = _userService.Read();
                            var list = task.Result;

                            if (list != null)
                            {
                                foreach (var u in list)
                                {
                                    if (u.Username == username && u.Password == password)
                                    {
                                        WriteLine("Logged in!");
                                        break;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            WriteLine($"An error occured: '{ex.Message}'");
                        }

                        break;
                }
            }
        }
    }
}