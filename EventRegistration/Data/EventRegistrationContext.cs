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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 設定外鍵關聯
            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Registrations)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Cascade); //刪除活動時一併刪除報名資料

            // 種子資料：建立測試活動
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    EventName = "資訊系迎新茶會",
                    EventType = "系內活動",
                    StartDate = new DateTime(2024, 9, 15),
                    EndDate = new DateTime(2024, 9, 15),
                    MaxParticipants = 100,
                    IsActive = true
                },
                new Event
                {
                    Id = 2,
                    EventName = "校園路跑活動",
                    EventType = "自由參加",
                    StartDate = new DateTime(2024, 10, 1),
                    EndDate = new DateTime(2024, 10, 1),
                    MaxParticipants = 200,
                    IsActive = true
                }

                );

        }

    }
}
