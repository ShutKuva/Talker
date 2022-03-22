using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using TalkerWeb.Models;

namespace TalkerWeb.Controllers
{
    public class AuthenticationController : Controller
    {
        HttpClient client;
        private string jwtToken; 

        public AuthenticationController(IOptions<API> apiKey)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(apiKey.Value.ApiKey ?? throw new ArgumentNullException(nameof(apiKey)));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            ViewData["Title"] = "Login";
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login([FromForm]LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Console.WriteLine(model.Login + " " + model.Password);
            return RedirectToRoute("mainPage");
        }

        public bool TryToAuthenticate(LoginModel model)
        {

        }

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
