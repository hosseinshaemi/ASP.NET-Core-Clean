using System.Reflection;
using HR_Management.MVC.Contracts;
using HR_Management.MVC.Services;
using HR_Management.MVC.Services.Base;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IClient, Client>(
    c => c.BaseAddress = new Uri(builder.Configuration.GetSection("ApiAddress").Value!)
);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
