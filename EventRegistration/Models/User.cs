using System.ComponentModel.DataAnnotations;

namespace EventRegistration.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "姓名為必填")]
        [StringLength(50)]
        [Display(Name = "姓名")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "入學年份")]
        [Range(90, 150, ErrorMessage = "入學年份須在 90-150 之間")]
        public int? EnrollmentYear { get; set; }

        [StringLength(1)]
        [Display(Name = "科系代號")]
        public string? Department { get; set; }

        [Required(ErrorMessage = "帳號為必填")]
        [StringLength(50)]
        [Display(Name = "帳號")]
        public string Account { get; set; } = string.Empty;

        [Required(ErrorMessage = "密碼為必填")]
        [StringLength(100)]
        [Display(Name = "密碼")]
        public string Password { get; set; } = string.Empty;

        [Phone(ErrorMessage = "請輸入有效的手機號碼")]
        [StringLength(20)]
        [Display(Name = "手機")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Email為必填")]
        [EmailAddress(ErrorMessage = "請輸入有效的Email格式")]
        [StringLength(100)]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "權限")]
        public UserRole Role { get; set; } = UserRole.Student;

        [Display(Name = "帳號建立日期")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [StringLength(500)]
        [Display(Name = "備註")]
        public string? Remarks { get; set; }

        // 導覽屬性：一個使用者可以有多筆報名
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }

    // 使用者角色列舉
    public enum UserRole
    {
        Student = 0,        // 學生
        Alumni = 1,         // 校友
        Teacher = 2,        // 老師
        SystemAdmin = 3     // 系統管理員
    }

    // 科系對應表（用於 Display）
    public static class DepartmentHelper
    {
        public static readonly Dictionary<string, string> Departments = new()
        {
            { "C", "資訊工程學系 (Computer Science)" },
            { "E", "電機工程學系 (Electrical Engineering)" },
            { "M", "機械工程學系 (Mechanical Engineering)" },
            { "B", "企業管理學系 (Business Administration)" },
            { "F", "織品管理學系 (Fabric Management)" },
            { "A", "應用美術學系 (Applied Arts)" },
            { "I", "工業設計學系 (Industrial Design)" },
            { "N", "護理學系 (Nursing)" }
        };

        public static string GetDepartmentName(string? code)
        {
            if (string.IsNullOrEmpty(code))
                return "未設定";

            return Departments.TryGetValue(code, out var name) ? name : "未知科系";
        }

        public static string GetRoleName(UserRole role)
        {
            return role switch
            {
                UserRole.Student => "學生",
                UserRole.Alumni => "校友",
                UserRole.Teacher => "老師",
                UserRole.SystemAdmin => "系統管理員",
                _ => "未知"
            };
        }
    }
}