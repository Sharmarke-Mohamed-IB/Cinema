using Microsoft.AspNetCore.Mvc;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Controllers
{
    public class MovieController : Controller
    {
        private readonly CinemaDBContext _context;

        public MovieController(CinemaDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }
    }
}
