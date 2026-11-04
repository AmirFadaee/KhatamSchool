using KhatamSchool_Application.Features.Commands;
using KhatamSchool_Application.Features.Queries;
using KhatamSchool_Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace KhatamSchool_website.Controllers.Public_Website
{
        public class DepartmentsController : Controller
        {
        private readonly IDepartmentsService _service;
        public DepartmentsController(IDepartmentsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Teachers()
            {
            var teachers = await _service.GetTeacherListItemAsync();
                return View(teachers);
            }

            public IActionResult Management()
            {
                return View();
            }

            public IActionResult Language()
            {
                return View();
            }

            public IActionResult Counseling()
            {
                return View();
            }

        [HttpPost]
        public async Task<IActionResult> SubmitTicket([FromBody] AddCounselingTicketCommand command)
        {
            try
            {
                var result = await _service.AddCounselingTicketAsync(command);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { succeded = false, message = ex.Message, detail = ex.InnerException?.Message });
            }

        }
    }
}
