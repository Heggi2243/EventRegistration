using System.ComponentModel.DataAnnotations;

namespace EventRegistration.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "活動名稱為必填")]
        [StringLength(100)]
        public string EventName { get; set; } = string.Empty;

        [Required(ErrorMessage = "請選擇活動類型")]
        public string EventType { get; set; } = string.Empty; // "系內活動" 或 "自由參加"

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Range(1, 500, ErrorMessage = "人數上限必須在 1-500 之間")]
        public int MaxParticipants { get; set; }

        public bool IsActive { get; set; } = true;

        // 導覽屬性：一個活動可以有多筆報名
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
