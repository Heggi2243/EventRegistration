using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; //下拉選單
using Microsoft.EntityFrameworkCore;
using EventRegistration.Data;
using EventRegistration.Models; //識別父級命名空間EventRegistrationContext

namespace EventRegistration.Controllers
{
    public class RegistrationsController : Controller
    {
        private readonly EventRegistrationContext _context;
        public RegistrationsController(EventRegistrationContext context)
        {
            _context = context;
        }

        // GET: Registrations/Create?eventId=1
        // 顯示報名表單
        public async Task<IActionResult> Create(int? eventId)
        {
            if (eventId == null)
            {
                return RedirectToAction("Index", "Events");
            }

            var eventItem = await _context.Events.FindAsync(eventId);
            if (eventItem == null || !eventItem.IsActive)
            {
                TempData["ErrorMessage"] = "此活動不存在或已關閉報名";
                return RedirectToAction("Index", "Events");
            }

            // 檢查是否已額滿
            var totalRegistered = await _context.Registrations
                .Where(r => r.EventId == eventId)
                .SumAsync(r => r.ParticipantCount);

            if (totalRegistered >= eventId.MaxParticipants)
            {
                TempData["ErrorMessage"] = "此活動報名已額滿";
                return RedirectToAction("Details", "Events", new { id = eventId });
            }

            ViewBag.EventName = eventItem.EventName;
            ViewBag.EventType = eventItem.EventType;
            ViewBag.EventId = eventId;

            return View(new Registration { EventId = eventId.Value });

        }

        // POST: Registrations/Create
        // 處理報名表單提交
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId, StudentName, StudentId, Email, ParticipantCount, IsVegetarian, ContactPhone, Remarks")])
        {
            // 重新檢查活動是否存在且開放
            var eventItem = await _context.Events.FindAsync(registration.EventId);
            if (eventItem == null || !eventItem.IsActive)
            {
                ModelState.AddModelError("", "此活動不存在或已關閉報名");
            }

            // 檢查是否會超過人數上限
            if (eventItem != null)
            {
                var totalRegistered = await _context.Registrations
                    .Where(r => r.EventId == registration.EventId)
                    .SumAsync(r => r.ParticipantCount);

                if (totalRegistered + registration.ParticipantCount > eventItem.MaxParticipants)
                {
                    ModelState.AddModelError("ParticipantCount",
                        $"報名人數超過上限，目前剩餘名額：{eventItem.MaxParticipants - totalRegistered} 人");
                }
            }

            // ModelState.IsValid驗證
            if (ModelState.IsValid)
            {
                registration.RegistrationDate = DateTime.Now;
                _context.Add(registration);
                await _context.SaveChangesAsync();

                // 報名成功，導向確認頁面
                return RedirectToAction(nameof(Confirmation), new { id = registration.Id });

            }

            // 驗證失敗，重新顯示表單並保留輸入資料
            ViewBag.EventName = eventItem?.EventName;
            ViewBag.EventType = eventItem?.EventType;
            ViewBag.EventId = registration.EventId;

            return View(registration);
        }

        // GET: Registrations/Confirmation/5
        // 報名成功確認頁面
        public async Task<IActionResult> Confirmation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.Event)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // GET: Registrations/Admin
        // 管理後台：顯示所有報名資料
        public async Task<IActionResult> Admin()
        {
            var registrations = await _context.Registrations
                .Include(r => r.Event)
                .OrderByDescending(r => r.RegistrationDate)
                .ToListAsync();

            return View(registrations);
        }

        // POST: Registrations/Delete/5
        // 取消報名
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration != null)
            {
                _context.Registrations.Remove(registration);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "報名已成功取消";
            }

            return RedirectToAction(nameof(Admin));
        }

    }
}
