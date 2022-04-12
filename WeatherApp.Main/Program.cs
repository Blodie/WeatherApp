using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using WeatherApp.Main.Data;
using WeatherApp.Main.Data.Models;
using WeatherApp.Main.Services.BackgroundServices;
using WeatherApp.Main.Services.Implementations;
using WeatherApp.Main.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IWeatherForecastAPIService, WeatherForecastAPIService>();
builder.Services.AddScoped<ICurrentWeatherAPIService, CurrentWeatherAPIService>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddHttpClient<ICurrentWeatherAPIService, CurrentWeatherAPIService>(client => client.BaseAddress = new Uri($"https://api.weatherbit.io/v2.0/current"));
builder.Services.AddHttpClient<IWeatherForecastAPIService, WeatherForecastAPIService>(client => client.BaseAddress = new Uri($"https://api.weatherbit.io/v2.0/forecast/daily"));

builder.Services.AddHostedService<ForecastUpdaterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
