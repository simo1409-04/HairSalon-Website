using HairSalon.Data;
using HairSalon.Data.Contracts;
using HairSalon.Services.Core.Contracts;
using HairSalon.Services.Core.Reservations;
using HairSalon.Services.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HairSalon.Services.Core.Hairdressers;
using HairSalon.Services.Core.Gallery;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
builder.Services.AddRazorPages();
string connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString") ?? throw new InvalidOperationException("Wrong or missing connection string!");

builder.Services.AddDbContext<HairSalonDb>(options =>
    options.UseSqlServer(connectionString));

builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireUppercase = true; 

        options.Password.RequireLowercase = true; 

        options.Password.RequireDigit = true; 

        options.Password.RequireNonAlphanumeric = true; 

        options.Password.RequiredLength = 6; 

        options.Password.RequiredUniqueChars = 1;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<HairSalonDb>();

// Check the account on every authenticated request so deleted employee sessions expire immediately.
builder.Services.Configure<SecurityStampValidatorOptions>(options =>
    options.ValidationInterval = TimeSpan.Zero);

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IServicesService, ServicesService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ReservationResult>();
builder.Services.AddScoped<IHairdresserService, HairdresserService>();
builder.Services.AddScoped<IGalleryService, GalleryService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
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


app.MapStaticAssets();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

app.Run();


