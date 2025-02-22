using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission6_Simmons.Models;
    

namespace Mission6_Simmons.Controllers;

public class HomeController : Controller
{
    private movieContext _context;

    public HomeController(movieContext temp)
    {
        _context = temp;
    }

    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetToKnowJoel()
    {
        return View();
    }

    [HttpGet]
    public IActionResult AddMovie()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddMovie(movie response)
    {
        _context.Movies.Add(response);
        _context.SaveChanges();

        return View(response);
    }

    public IActionResult ViewMovie()
    {
        var movie = _context.Movies
            .Include(m => m.Category)
            .OrderBy(m => m.Title)
            .ToList();
        return View(movie);
    }
    
    public IActionResult EditMovie(int movieId)
    {
        var movieToEdit = _context.Movies
            .Where(x => x.MovieId == 1);
        return View("AddMovie");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var movie = _context.Movies.Find(id);
        if (movie == null)
        {
            return NotFound();
        }
        return View(movie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, movie updatedMovie)
    {
        Console.WriteLine($"Received ID: {id}, Movie ID: {updatedMovie.MovieId}");

        if (id != updatedMovie.MovieId)
        {
            Console.WriteLine("Error: ID mismatch!");
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var existingMovie = _context.Movies.FirstOrDefault(m => m.MovieId == id);

                if (existingMovie == null)
                {
                    Console.WriteLine("Error: Movie not found in the database!");
                    return NotFound();
                }

                // Update properties
                existingMovie.Title = updatedMovie.Title;
                existingMovie.Year = updatedMovie.Year;
                existingMovie.Edited = updatedMovie.Edited;
                existingMovie.CopiedToPlex = updatedMovie.CopiedToPlex;
                existingMovie.CategoryId = updatedMovie.CategoryId;

                _context.SaveChanges();  // No explicit transaction needed in SQLite
                Console.WriteLine("Update successful!");

                return RedirectToAction(nameof(ViewMovie));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database update failed: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("ModelState is invalid!");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"Validation Error: {error.ErrorMessage}");
            }
        }

        return View(updatedMovie);
    }

    public IActionResult Delete(int id)
    {
        var movie = _context.Movies.Find(id);
        if (movie == null)
        {
            return NotFound();
        }
        return View(movie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int movieId)
    {
        var movie = _context.Movies.Find(movieId);

        if (movie == null)
        {
            return NotFound();
        }
        
        _context.Movies.Remove(movie);
        _context.SaveChanges();
        
        return RedirectToAction(nameof(ViewMovie));
    }
}