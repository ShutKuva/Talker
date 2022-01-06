using BLL.Abstractions.Interfaces;
using Core.Models;

namespace ConsoleApp3
{
    public class App
    {
        const string REGISTER = "-r",

        private readonly IUserService _userService;

        public App(IUserService userService)
        {
            _userService = userService;
        }
        
        public void StartApp()
        {
            while (true)
            {

            }
        }
    }
}