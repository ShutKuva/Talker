using BLL.Abstractions.Interfaces;
using BLL.Abstractions.Interfaces.Validators;
using Core.Models;
using PresentationLayer.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Abstractions.AbstractClasses
{
    public abstract class PLServiceWithPasswordValidations : IPLService
    {
        public abstract Task Execute();

        protected void GetPassword(User newUser, IHashHandler hashHandler, IPasswordValidator passwordValidator)
        {
            Console.WriteLine("Write new password:");
            string newPassword = Console.ReadLine();
            string passwordCorrection;

            do
            {
                passwordCorrection = passwordValidator.IsItValidPassword(newPassword);
                if (!string.IsNullOrWhiteSpace(passwordCorrection))
                {
                    Console.WriteLine(passwordCorrection);
                    newPassword = Console.ReadLine();
                }
            } while (!string.IsNullOrWhiteSpace(passwordCorrection));

            newUser.Password = hashHandler.GetHash(newPassword);
        }
    }
}
