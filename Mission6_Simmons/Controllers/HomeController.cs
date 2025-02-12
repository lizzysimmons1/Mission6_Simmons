using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
        _context.Movie.Add(response);
        _context.SaveChanges();
        
        return View(response);
    }
}