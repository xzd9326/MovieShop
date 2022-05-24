﻿using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public async Task<IActionResult> Details(int id)
        {
            // go to movies table and get the movie details by ID
            // connect to SQL sever and execute the SQL query
            // select * from Movie where id = 2;
            // get the movies entity (object)
            // Repositories => Data access Logic
            // Services => Business Logic
            // Controllers action methods => Services methods => Repository methods => SQL database
            // get the mode data from the services and send the data to the views (M)
            // Onion architecture or N-Layer architecture
            var movie = await _movieService.GetMovieDetails(id);
            return View(movie);
        }
    }
}
