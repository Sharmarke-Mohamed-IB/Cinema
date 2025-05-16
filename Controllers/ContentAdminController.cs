using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cinema.Controllers;
public class ContentAdminController : Controller
{
    private readonly CinemaDBContext _dbContext;
    public ContentAdminController(CinemaDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IActionResult Index()
    {
        return View();
    }
    public async Task<IActionResult> ShowAllMovies()
    {
        return View(await _dbContext.Movies.ToListAsync());
    }
    public async Task<IActionResult> ShowScreenings()
    {
        return View(await _dbContext.Screenings.ToListAsync());
    }
    public IActionResult AddNewMovie()
    {
        return View();
    }
    // POST: Movies/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddNewMovie([Bind("Id,Title,Type,Director,Summary,Length")] Movie movie)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Add(movie);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }
    public IActionResult AddNewScreening(int id)
    {
        List<SelectListItem> MovieId = new List<SelectListItem>();
        SelectListItem m = new SelectListItem();
        m.Text = id.ToString();
        m.Value = id.ToString();
        MovieId.Add(m);
        ViewBag.MovieId = MovieId;

        List<SelectListItem> HallId = new List<SelectListItem>();

        foreach(Hall hall in _dbContext.Halls) 
        {
            HallId.Add(new SelectListItem { Text = hall.Id.ToString(), Value = hall.Id.ToString() });
        }

        ViewBag.HallId = HallId;

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddNewScreening(String MovieId, String HallId, DateTime DatetimeOfPlay)
    {
        int movie_id = int.Parse(MovieId);
        int hall_id = int.Parse(HallId);
        Screening scr = new Screening();
        scr.MovieId = movie_id;
        scr.HallId = hall_id;
        scr.DatetimeOfPlay = DatetimeOfPlay;
        System.Console.WriteLine(ModelState.IsValid.ToString() + scr.MovieId.ToString() + scr.HallId.ToString() + scr.DatetimeOfPlay.ToString());
        if (ModelState.IsValid)
        {
            _dbContext.Add(scr);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(scr);
    }
    // GET: Users/Delete/5
    public async Task<IActionResult> DeleteScreening(int? id)
    {
        if (id == null || _dbContext.Screenings == null)
        {
            return NotFound();
        }

        var scr = await _dbContext.Screenings
            .FirstOrDefaultAsync(m => m.Id == id);
        if (scr == null)
        {
            return NotFound();
        }

        return View(scr);
    }

    // POST: Users/Delete/5
    [HttpPost, ActionName("DeleteScreening")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_dbContext.Screenings == null)
        {
            return Problem("Entity set 'CinemaDBContext.Screenings'  is null.");
        }
        var scr = await _dbContext.Screenings.FindAsync(id);
        if (scr != null)
        {
            _dbContext.Screenings.Remove(scr);
        }

        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    // GET: Screenings/Edit/5
    public async Task<IActionResult> EditScreening(int? id)
    {
        if (id == null || _dbContext.Screenings == null)
        {
            return NotFound();
        }

        var screening = await _dbContext.Screenings.FindAsync(id);
        if (screening == null)
        {
            return NotFound();
        }
        ViewData["HallId"] = new SelectList(_dbContext.Halls, "Id", "Id", screening.HallId);
        ViewData["MovieId"] = new SelectList(_dbContext.Movies, "Id", "Id", screening.MovieId);
        return View(screening);
    }

    // POST: Screenings/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,HallId,DatetimeOfPlay")] Screening screening)
    {
        if (id != screening.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _dbContext.Update(screening);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScreeningExists(screening.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["HallId"] = new SelectList(_dbContext.Halls, "Id", "Id", screening.HallId);
        ViewData["MovieId"] = new SelectList(_dbContext.Movies, "Id", "Id", screening.MovieId);
        return View(screening);
    }
    private bool ScreeningExists(int id)
    {
        return (_dbContext.Screenings?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
