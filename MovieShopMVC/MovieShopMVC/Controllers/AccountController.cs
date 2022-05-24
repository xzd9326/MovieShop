using ApplicationCore.Contracts.Services;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // showing the empty page
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // when user clicks on Submit/register button
        // 
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            // save the info in User Table
            try
            {
                var user = await _accountService.RegisterUser(model);
            }
            catch (ConflictException)
            {
                throw;
                // logging the exceptions later to text /json files
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            // Model Binding, it looks at the incoming request from clinet/browser and look at the info and if it matches with the properties of the model  it will get the values automatically

            // http 10:00 AM => email/pw => create something so that, auth cookie ( 2 hrs) 
            // http 10:05 AM => user/purchases
            // create a cookie, userid, email, -> encrypted, expiration time
            // each and every time you make an http request the cookies are sent to server in http
            // Cookie based authentication

            // 1:00 PM => user/purchases, redirect to the login page

            try
            {
                var user = await _accountService.LoginUser(model.Email, model.Password);
                if (user == null)
                {
                    throw new Exception("Email does not exists");
                }

                // claims => things that represent you
                // Driver Licence -> First Name, Last Name, DOF
                // Licence -> For entering some special building
                // Claim called Admin Role to enter admin pages

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToShortDateString()),
                    new Claim("Language", "English")
                };

                // Identity
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // create the cookie with above claims
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            }
            catch (Exception)
            {
                return View();
                throw;
            }
            // return View();
            // ASP.NET, how long the cookie is gonna be valid
            // Name of the cookie
            return LocalRedirect("~/");
        }
    }
}
