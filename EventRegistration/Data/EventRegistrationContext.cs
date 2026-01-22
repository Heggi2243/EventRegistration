using Microsoft.EntityFrameworkCore;
using EventRegistration.Models;

namespace EventRegistration.Data
{
    public class EventRegistrationContext : DbContext
    {
        public EventRegistrationContext(DbContextOptions<EventRegistrationContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<User> Users { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Event-Registration 關聯
            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Registrations)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // ⭐ User-Registration 關聯
            modelBuilder.Entity<Registration>()
                .HasOne(r => r.User)
                .WithMany(u => u.Registrations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);  // 避免連鎖刪除使用者時刪除報名資料

            // 設定 Account 為唯一索引
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Account)
                .IsUnique();

            // 種子資料：活動
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    EventName = "資訊系迎新茶會",
                    EventType = EventType.DepartmentOnly,
                    Department = "C",
                    StartDate = new DateTime(2024, 9, 15),
                    EndDate = new DateTime(2024, 9, 15),
                    MaxParticipants = 100,
                    IsActive = true
                },
                new Event
                {
                    Id = 2,
                    EventName = "校園路跑活動",
                    EventType = EventType.OpenToAll, 
                    Department = null,
                    StartDate = new DateTime(2024, 10, 1),
                    EndDate = new DateTime(2024, 10, 1),
                    MaxParticipants = 200,
                    IsActive = true
                },
                new Event
                {
                    Id = 3,
                    EventName = "企管系職涯講座",
                    EventType = EventType.DepartmentOnly,
                    Department = "B",
                    StartDate = new DateTime(2024, 11, 20),
                    EndDate = new DateTime(2024, 11, 20),
                    MaxParticipants = 80,
                    IsActive = true
                }
            );

            // ⭐ 種子資料：使用者
            modelBuilder.Entity<User>().HasData(
                // 系統管理員
                new User
                {
                    Id = 1,
                    Name = "系統管理員",
                    Account = "admin",
                    Password = "admin123",  // 實際應使用雜湊加密
                    Email = "admin@school.edu.tw",
                    Role = UserRole.SystemAdmin,
                    CreatedAt = new DateTime(2020, 1, 1),
                    Remarks = "系統預設管理員帳號"
                },

                // 老師
                new User
                {
                    Id = 2,
                    Name = "王大明",
                    Account = "T001",
                    Password = "teacher123",
                    Email = "wang@school.edu.tw",
                    Phone = "0912345601",
                    Role = UserRole.Teacher,
                    Department = "C",
                    CreatedAt = new DateTime(2015, 8, 1),
                    Remarks = "資訊工程系教授"
                },
                new User
                {
                    Id = 3,
                    Name = "李小華",
                    Account = "T002",
                    Password = "teacher123",
                    Email = "lee@school.edu.tw",
                    Phone = "0912345602",
                    Role = UserRole.Teacher,
                    Department = "B",
                    CreatedAt = new DateTime(2018, 8, 1),
                    Remarks = "企業管理系副教授"
                },

                // 學生 - 資訊系
                new User
                {
                    Id = 4,
                    Name = "陳曉明",
                    Account = "C11200001",  // 資訊系、112年入學、學士、日間部、001號
                    Password = "student123",
                    Email = "c11200001@student.school.edu.tw",
                    Phone = "0912345611",
                    Role = UserRole.Student,
                    Department = "C",
                    EnrollmentYear = 112,
                    CreatedAt = new DateTime(2023, 9, 1)
                },
                new User
                {
                    Id = 5,
                    Name = "林佳穎",
                    Account = "C11200015",  // 資訊系、112年入學、學士、日間部、015號
                    Password = "student123",
                    Email = "c11200015@student.school.edu.tw",
                    Phone = "0912345612",
                    Role = UserRole.Student,
                    Department = "C",
                    EnrollmentYear = 112,
                    CreatedAt = new DateTime(2023, 9, 1)
                },

                // 學生 - 織品管理系
                new User
                {
                    Id = 6,
                    Name = "張雅婷",
                    Account = "F11200022",  // 織品管理系、112年入學、學士、日間部、022號
                    Password = "student123",
                    Email = "f11200022@student.school.edu.tw",
                    Phone = "0912345613",
                    Role = UserRole.Student,
                    Department = "F",
                    EnrollmentYear = 112,
                    CreatedAt = new DateTime(2023, 9, 1)
                },

                // 學生 - 企管系
                new User
                {
                    Id = 7,
                    Name = "劉志強",
                    Account = "B11100008",  // 企管系、111年入學、學士、日間部、008號
                    Password = "student123",
                    Email = "b11100008@student.school.edu.tw",
                    Phone = "0912345614",
                    Role = UserRole.Student,
                    Department = "B",
                    EnrollmentYear = 111,
                    CreatedAt = new DateTime(2022, 9, 1)
                },

                // 學生 - 機械系 碩士生
                new User
                {
                    Id = 8,
                    Name = "黃建國",
                    Account = "M11310003",  // 機械系、113年入學、碩士、日間部、003號
                    Password = "student123",
                    Email = "m11310003@student.school.edu.tw",
                    Phone = "0912345615",
                    Role = UserRole.Student,
                    Department = "M",
                    EnrollmentYear = 113,
                    CreatedAt = new DateTime(2024, 9, 1),
                    Remarks = "碩士班研究生"
                },

                // 學生 - 電機系 進修部
                new User
                {
                    Id = 9,
                    Name = "吳明輝",
                    Account = "E11101010",  // 電機系、111年入學、學士、進修部、010號
                    Password = "student123",
                    Email = "e11101010@student.school.edu.tw",
                    Phone = "0912345616",
                    Role = UserRole.Student,
                    Department = "E",
                    EnrollmentYear = 111,
                    CreatedAt = new DateTime(2022, 9, 1),
                    Remarks = "進修部學生"
                },

                // 校友
                new User
                {
                    Id = 10,
                    Name = "周佩君",
                    Account = "C10900025",  // 資訊系、109年入學（已畢業）
                    Password = "alumni123",
                    Email = "chou.alumni@gmail.com",
                    Phone = "0912345617",
                    Role = UserRole.Alumni,
                    Department = "C",
                    EnrollmentYear = 109,
                    CreatedAt = new DateTime(2020, 9, 1),
                    Remarks = "2024年畢業校友"
                },
                new User
                {
                    Id = 11,
                    Name = "鄭家豪",
                    Account = "B10800012",  // 企管系、108年入學（已畢業）
                    Password = "alumni123",
                    Email = "cheng.alumni@gmail.com",
                    Phone = "0912345618",
                    Role = UserRole.Alumni,
                    Department = "B",
                    EnrollmentYear = 108,
                    CreatedAt = new DateTime(2019, 9, 1),
                    Remarks = "2023年畢業校友，現任職科技業"
                }
            );
        }
    }
}