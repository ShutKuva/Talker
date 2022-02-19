using Microsoft.AspNetCore.Mvc;

namespace TalkerWeb.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Main";
            return View();
        }
    }
}
