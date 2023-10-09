using e_Commerce_Test_Site.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<StoreUserContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StoreUserContext")));

builder.Services.AddSqlServer<StoreUserContext>(builder.Configuration.GetConnectionString("StoreUserContext"), options => options.EnableRetryOnFailure());

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<StoreUserContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/auth/signin");
    options.AccessDeniedPath = new PathString("/");
});

builder.Services.AddAuthorization(options =>
    options.AddPolicy("adminpolicy", policy =>
        policy.RequireAuthenticatedUser().
            RequireClaim(ClaimTypes.Email, "admin@admin.com")));

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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
