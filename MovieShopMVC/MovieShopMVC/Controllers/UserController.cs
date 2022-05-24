using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        // Show all the movies purchased by currently loged in user
        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            // First whether user is loged in
            // if the user is not loged in,
            // redirect to login Page
            // 10:00 AM user email/password => something that can be used at 10:05
            // cookies, authentication cookies that can be used across http request and check whether user is loged in or not.
            // cokkies -> location?
            // 10:05 he/she calls user/purchases
            // look for the authCookie and look for exp time and get the userid
            // Http Request is independent of each other
            // userid, go to purchase table and get al the movies purchased
            // display as movie cards, use movie card partial view

            // var data = this.HttpContext.Request.Cookies["MovieShopAuthCookie"];
            //var isLogedIn = this.HttpContext.User.Identity.IsAuthenticated;
            //if (!isLogedIn)
            //{
            //    // redirect to login page
            //}
            // Filters in ASP.NET
            var userId = Convert.ToInt32(this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            // call the UserService -> GetMoviesPurchasedByUser(int userId) -> List<MovieCard>
            // send it to databse
            // decrypt the cookie and get the userid from claims and expiration time from the cookie
            // use the userid to go to database and get the movies purchased
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var userId = Convert.ToInt32(this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return View();
        }
        public async Task<IActionResult> Reviews()
        {
            var userId = Convert.ToInt32(this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return View();
        }
    }
}
