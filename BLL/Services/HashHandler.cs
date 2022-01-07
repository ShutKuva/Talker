using System;
using BLL.Abstractions.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Services
{
    public class HashHandler: IHashHandler
    {
        public string GetHash(string data)
        {
            SHA256 decryptor = SHA256.Create();

            byte[] bytes = decryptor.ComputeHash(Encoding.UTF8.GetBytes(data));

            var sb = new StringBuilder();

            foreach (var bytE in bytes)
            {
                sb.Append(bytE.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
