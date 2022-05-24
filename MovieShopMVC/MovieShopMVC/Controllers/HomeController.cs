using ApplicationCore.Contracts.Services;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;
using System.Diagnostics;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
            // code to be relied on abstarctions rather than concrete types
        }

        // Action methods
        // https://localhost:7211/home/index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // newing up
            // we can have some higher level framework to create instances
            // var movieService = new MovieService();
            var movieCards = await _movieService.GetTop30GrossingMovies();
            // passing the data from Controller action method to View
            return View(movieCards);
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        // https://localhost:7211/home/TopRatedMovies
        public IActionResult TopRatedMovies()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}