using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventRegistration.Data;

//告知系統：這份檔案存放在哪裡(位置識別)
namespace EventRegistration.Controllers
{
    // public class EventsController (身分識別)
    // 寫" : Controller "的意思是繼承(工具識別，讓系統知道給EventsController甚麼工具)
    public class EventsController : Controller
    {
        /* 這裡是在宣告一個私有的位置。
         * readonly: 表示_context這個變數只能在建構子中賦值，防止在執行過程意外把資料庫連線換掉
         * 私有變數通常以底線最為開頭，來跟一般變數區隔。*/
        private readonly EventRegistrationContext _context;

        // 建構子注入DbContext
        // 當Controller啟動時，系統會從外面把連線「注入」進來
        public EventsController(EventRegistrationContext context) 
        { 
            _context = context;  // 把外面傳進來的東西存到私有的位置。
        }

        // GET: Events
        // async Task<>等待資料庫回應時可以先去處理其他請求
        public async Task<IActionResult> Index()
        {
            // 取得活動列表
            var events = await _context.Events
                //對於每一個e(元素)，回傳e.IsActive的結果(想像成迴圈的簡化版)
                .Where(e => e.IsActive) // 只顯示開放中的活動
                .OrderBy(e => e.StartDate)
                .ToListAsync();

            return View(events);
        }

        // GET: Events/Details/5 (活動詳情)
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var eventItem = await _context.Events
                .Include(e => e.Registrations) // 包含報名資料，用來計算已報名人數
                 // 取得查詢結果的第一個元素
                .FirstOrDefaultAsync(m => m.Id == id);

            if (eventItem == null)
            {
                return NotFound();
            }

            ViewBag.TotalRegistered = eventItem.Registrations
                // 統計參與人數
                .Sum(r => r.ParticipantCount);

            return View(eventItem);
        }

    }
}
