using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventRegistration.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EventType = table.Column<int>(type: "int", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxParticipants = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EnrollmentYear = table.Column<int>(type: "int", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Account = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParticipantCount = table.Column<int>(type: "int", nullable: false),
                    IsVegetarian = table.Column<bool>(type: "bit", nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registrations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registrations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Department", "EndDate", "EventName", "EventType", "IsActive", "MaxParticipants", "StartDate" },
                values: new object[,]
                {
                    { 1, "C", new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "資訊系迎新茶會", 0, true, 100, new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, null, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "校園路跑活動", 1, true, 200, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "B", new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "企管系職涯講座", 0, true, 80, new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Account", "CreatedAt", "Department", "Email", "EnrollmentYear", "Name", "Password", "Phone", "Remarks", "Role" },
                values: new object[,]
                {
                    { 1, "admin", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@school.edu.tw", null, "系統管理員", "admin123", null, "系統預設管理員帳號", 3 },
                    { 2, "T001", new DateTime(2015, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C", "wang@school.edu.tw", null, "王大明", "teacher123", "0912345601", "資訊工程系教授", 2 },
                    { 3, "T002", new DateTime(2018, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", "lee@school.edu.tw", null, "李小華", "teacher123", "0912345602", "企業管理系副教授", 2 },
                    { 4, "C11200001", new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C", "c11200001@student.school.edu.tw", 112, "陳曉明", "student123", "0912345611", null, 0 },
                    { 5, "C11200015", new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C", "c11200015@student.school.edu.tw", 112, "林佳穎", "student123", "0912345612", null, 0 },
                    { 6, "F11200022", new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "F", "f11200022@student.school.edu.tw", 112, "張雅婷", "student123", "0912345613", null, 0 },
                    { 7, "B11100008", new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", "b11100008@student.school.edu.tw", 111, "劉志強", "student123", "0912345614", null, 0 },
                    { 8, "M11310003", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "M", "m11310003@student.school.edu.tw", 113, "黃建國", "student123", "0912345615", "碩士班研究生", 0 },
                    { 9, "E11101010", new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "E", "e11101010@student.school.edu.tw", 111, "吳明輝", "student123", "0912345616", "進修部學生", 0 },
                    { 10, "C10900025", new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C", "chou.alumni@gmail.com", 109, "周佩君", "alumni123", "0912345617", "2024年畢業校友", 1 },
                    { 11, "B10800012", new DateTime(2019, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", "cheng.alumni@gmail.com", 108, "鄭家豪", "alumni123", "0912345618", "2023年畢業校友，現任職科技業", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_EventId",
                table: "Registrations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_UserId",
                table: "Registrations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Account",
                table: "Users",
                column: "Account",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
