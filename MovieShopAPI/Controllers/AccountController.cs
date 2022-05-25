using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserRepository _userRepository;
        public AccountController(IAccountService accountService, IUserRepository userRepository)
        {
            _accountService = accountService;
            _userRepository = userRepository;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                // http status code 400 Bad Request
                return BadRequest();
            }
            var user = await _accountService.RegisterUser(model);
            
            return Ok(user);
        }

        [Route("check-email")]
        [HttpGet]
        public async Task<IActionResult> CheckEmail(string email)
        {
            var dbEmail = await _userRepository.GetUserByEmail(email);
            if (dbEmail == null)
            {
                return Ok("Email Does Not Exist");
            }
            return Ok("Email Already Exists");
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _accountService.LoginUser(email, password);
            if (user == null)
            {
                return BadRequest( new { message = "Username or password is incorrect" });
            }
            return Ok(user);
        }
    }
}
