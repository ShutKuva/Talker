using BLL.Abstractions.Interfaces;
using Core.Models;

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
            var user = new User();
            user.Id = 1;
            user.Name = "Pavel";
            user.Surname = "Petrenko";
            user.Age = 32;

            //hello github!
            
            _userService.Create(user);
        }
    }
}