using KhatamSchool_Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace KhatamSchool_website.Controllers.Public_Website
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService) => _accountService = accountService;

        //public IActionResult Register()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public class AddUserRequest
        {
            public string Fullname { get; set; } = null!;
            public string Username { get; set; } = null!;
            public string Password { get; set; } = null!;
        }
        public class LoginRequest
        {
            public string Username { get; set; } = null!;
            public string Password { get; set; } = null!;
        }
        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] AddUserRequest request)
        {
            var result = await _accountService.CreateUserAsync(request.Fullname, request.Username, request.Password);
            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var result = await _accountService.LoginAsync(request.Username, request.Password);
            return Json(result);
        }


    }
}
