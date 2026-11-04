using KhatamSchool_Application.Features.Commands;
using KhatamSchool_Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KhatamSchool_website.Controllers.Public_Website
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult AddTeacher()
        {
            return View();
        }
        public async Task<IActionResult> AddAchievementItem()
        {
            var model = await _adminService.GetAddAchievementItemPageAsync();
            return View(model);
        }
        public IActionResult AddExecutiveStaff()
        {
            return View();
        }
        public IActionResult AddAchievementCategory()
        {
            return View();
        }
        public IActionResult AddNews()
        {
            return View();
        }

        //Add

        [HttpPost]
        public async Task<IActionResult> AddTeacherAsync(AddTeacherCommand command, IFormFile ProfileImage)
        {
            var result = await _adminService.AddTeacherAsync(command, ProfileImage);
            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewsAsync([FromForm] AddNewsCommand command)
        {
            var result = await _adminService.AddNewsAsync(command);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAchievementCategoryAsync(AddAchievementCategoryCommand command)
        {
            var response = await _adminService.AddAchievementCategoryAsync(command);
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddExecutiveStaffAsync(AddExecutiveStaffCommand command, IFormFile ProfileImage)
        {
            var result = await _adminService.AddExecutiveStaffAsync(command, ProfileImage);
            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddAchievementItemAsync([FromForm] AddAchievementItemCommand command)
        {
            var response = await _adminService.AddAchievementItemAsync(command);
            return Json(response);
        }


        //List
        public async Task<IActionResult> AchievementCategoryList()
        {
            var list = await _adminService.GetAchievementCategoryListAsync();
            return View(list);
        }
        public async Task<IActionResult> AchievementItemList()
        {
            var list = await _adminService.GetAchievementItemListAsync();
            return View(list);
        }
        public async Task<IActionResult> TeachersList()
        {
            var list = await _adminService.GetTeacherListAsync();
            return View(list);
        }
        public async Task<IActionResult> ExecutiveStaffList()
        {
            var list = await _adminService.GetExecutiveStaffListAsync();
            return View(list);
        }
        public async Task<IActionResult> CounselingTicketList()
        {
            var list = await _adminService.GetCounselingTicketListAsync();
            return View(list);
        }
        public async Task<IActionResult> NewsList()
        {
            var list = await _adminService.GetNewsListAsync();
            return View(list);
        }

        //Delete
        public async Task<IActionResult> DeleteTeacherAsync(long Id)
        {
            var response = await _adminService.DeleteTeacherAsync(Id);
            return Json(response);
        }
        public async Task<IActionResult> DeleteNewsAsync(long Id)
        {
            var response = await _adminService.DeleteNewsAsync(Id);
            return Json(response);
        }
        public async Task<IActionResult> DeleteExecutiveStaffAsync(long Id)
        {
            var response = await _adminService.DeleteExecutiveStaffAsync(Id);
            return Json(response);
        }
        public async Task<IActionResult> DeleteAchievementCategoryAsync(long Id)
        {
            var response = await _adminService.DeleteAchievementCategoryAsync(Id);
            return Json(response);
        }
        public async Task<IActionResult> DeleteAchievementItemAsync(long Id)
        {
            var response = await _adminService.DeleteAchievementItemAsync(Id);
            return Json(response);
        }

        //Update
        public async Task<IActionResult> UpdateTeacher(long id)
        {
            var model =await _adminService.GetTeacherDataByIdAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTeacherAsync([FromForm] UpdateTeacherCommand command)
        {
            var result = await _adminService.UpdateTeacherAsync(command);
            return Json(result);
        }
    }
}
