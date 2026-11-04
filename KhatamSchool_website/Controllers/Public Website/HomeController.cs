using KhatamSchool_Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace KhatamSchool_website.Controllers.Public_Website
{
    public class HomeController : Controller
    {
        private readonly IHomeService _service;
        public HomeController(IHomeService homeService)
        {
            _service = homeService;
        }
        public async Task<IActionResult> Home()
        {
            var model = await _service.GetHomeDataAsync();

            return View(model);
        }
        public async Task<IActionResult> Achievements(long CategoryId)
        {
            var achievements = await _service.GetAchievementsViewAsync(CategoryId);
            return View(achievements);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

    }
}
