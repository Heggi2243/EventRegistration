using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventRegistration.Data;
using EventRegistration.Models;


namespace EventRegistration.Controllers
{
    public class MyEventsController : Controller
    {
        private readonly EventRegistrationContext _context;

        public MyEventsController(EventRegistrationContext context)
        {
            _context = context;
        }

        // GET: MyEvents?studentId=A1234567
        // 學生的活動頁面
        public IActionResult Index(string? studentId)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                //返回登入頁
                return View("Login");
            }

            ViewBag.StudentId = studentId;
            return View();

        }

        // AJAX API: 取得"已報名"活動
        [HttpGet]
        public async Task<IActionResult> GetRegistered(string studentId)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                return Json(new { success = false, message = "學號不可為空" });
            }

            var registrations = await _context.Registrations
                .Include(r => r.Event)
                .Where(r => r.StudentId == studentId &&
                           r.Status == 1 &&
                           r.Event!.EndDate >= DateTime.Today) // 活動還沒結束
                .OrderBy(r => r.Event!.StartDate)
                .Select(r => new
                {
                    r.Id,
                    r.Event!.EventName,
                    r.Event.EventType,
                    r.Event.StartDate,
                    r.Event.EndDate,
                    r.ParticipantCount,
                    r.IsVegetarian,
                    r.RegistrationDate
                })
                .ToListAsync();

            return Json(registrations);

        }
    }

}