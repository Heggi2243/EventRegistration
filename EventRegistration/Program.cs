using Microsoft.EntityFrameworkCore;
using EventRegistration.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. 加入 MVC 服務 (包含 Controller 和 View 支援)
builder.Services.AddControllersWithViews();

// 2. 註冊 Entity Framework Core 的 DbContext
builder.Services.AddDbContext<EventRegistrationContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// 3. 設定 HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();  // 啟用靜態檔案 (CSS, JS, 圖片等)

app.UseRouting();

app.UseAuthorization();

// 4. 設定預設路由
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
