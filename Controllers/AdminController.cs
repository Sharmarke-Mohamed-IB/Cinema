using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
