using System.ComponentModel.DataAnnotations;

namespace EventRegistration.Models
{

    // 報名狀態
    public enum RegistrationStatus
    {
        [Display(Name = "已取消")]
        Cancelled = 0,

        [Display(Name = "已報名")]
        Registered = 1,

        [Display(Name = "缺席")]
        Absent = 2
    }

    public class Registration
    {
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required(ErrorMessage ="姓名為必填")]
        [StringLength(50)]
        [Display(Name = "學生姓名")]
        public string StudentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "學號為必填")]
        [StringLength(20)]
        [Display(Name = "學號")]
        public string StudentId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email為必填")]
        [EmailAddress(ErrorMessage = "請輸入有效的Email格式")]
        [Display(Name = "電子郵件")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "參加人數為必填")]
        [Range(1, 10, ErrorMessage = "參加人數必須在 1-10 之間")]
        [Display(Name = "參加人數")]
        public int ParticipantCount { get; set; } = 1;

        [Display(Name = "是否需要素食")]
        public bool IsVegetarian { get; set; }

        [Phone(ErrorMessage = "請輸入有效的電話號碼")]
        [Display(Name = "聯絡電話")]
        public string? ContactPhone { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        public string? Remarks { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public RegistrationStatus Status { get; set; } = RegistrationStatus.Registered;

        // 導覽屬性：關聯到活動
        public Event? Event { get; set; }

    }


}
