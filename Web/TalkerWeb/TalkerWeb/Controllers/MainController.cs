using Microsoft.AspNetCore.Mvc;

namespace TalkerWeb.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {

            }
            ViewData["Title"] = "Main";
            return View();
        }
    }
}
