using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace Cinema.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CinemaDBContext _dbContext;
        public CustomerController(CinemaDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowAvailableMovies()
        {
            List<Movie> avail_movies = new List<Movie>();

            List<int> ids = new List<int>();
            foreach (Screening scr in _dbContext.Screenings)
            {
                ids.Add(scr.MovieId);
            }

            foreach (Movie movie in _dbContext.Movies)
            {
                if (ids.Contains(movie.Id))
                {
                    avail_movies.Add(movie);
                }
            }

            return View(avail_movies);
        }
        public IActionResult BookTicket(int id)
        {

            List<Screening> avail_scr = new List<Screening>();

            foreach (Screening scr in _dbContext.Screenings)
            {
                if(scr.MovieId == id)
                {
                    avail_scr.Add(scr);
                }
            }
            return View(avail_scr);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookTicket(int Id, int nos)
        {
            Reservation rsv = new Reservation();
            rsv.CostumerId = 1;
            rsv.ScrId = Id;
            rsv.NumberOfSeats = nos;
            if (ModelState.IsValid)
            {
                _dbContext.Add(rsv);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rsv);
        }
        public async Task<IActionResult> BookingHistory()
        {
            return View(await _dbContext.Reservations.Where(u => u.CostumerId == 1).ToListAsync());
        }
        //(u => u.CostumerId == currentUserId)
    }
}
