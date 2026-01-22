using System.ComponentModel.DataAnnotations;

namespace EventRegistration.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "活動名稱為必填")]
        [StringLength(100)]
        [Display(Name = "活動名稱")]
        public string EventName { get; set; } = string.Empty;

        [Required(ErrorMessage = "請選擇活動類型")]
        [Display(Name = "活動類型")]
        public EventType EventType { get; set; } = EventType.OpenToAll;

        // 只有系內活動才需要填寫
        [StringLength(1)]
        [Display(Name = "所屬科系")]
        public string? Department { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "開始日期")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "結束日期")]
        public DateTime EndDate { get; set; }

        [Range(1, 500, ErrorMessage = "人數上限必須在 1-500 之間")]
        [Display(Name = "人數上限")]
        public int MaxParticipants { get; set; }

        [Display(Name = "是否開放報名")]
        public bool IsActive { get; set; } = true;

        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }

    // 活動類型
    public enum EventType
    {
        [Display(Name = "系內活動")]
        DepartmentOnly = 0,

        [Display(Name = "自由參加")]
        OpenToAll = 1
    }

    // Helper: View顯示中文
    public static class EventTypeHelper
    {
        public static string GetDisplayName(EventType type)
        {
            return type switch
            {
                EventType.DepartmentOnly => "系內活動",
                EventType.OpenToAll => "自由參加",
                _ => "未知"
            };
        }
    }
}