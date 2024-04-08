using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using Vehicle_Rent.Data;
using Vehicle_Rent.Repository.Specific;
using Vehicle_Rent.Services.VehicleCatalogue;
using Vehicle_Rent.Services.VehicleRent;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMvc().AddRazorRuntimeCompilation();

builder.Services.AddMemoryCache();
#region DataAccess
builder.Services.AddDbContext<CarRentalDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

#region Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleCopyRepository, VehicleCopyRepository>();
builder.Services.AddScoped<IRentalItemRepository, RentalItemRepository>();
builder.Services.AddScoped<IVModelRepository, VModelRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
#endregion

#region Service
builder.Services.AddScoped<IVehicleCatalogueService, VehicleCatalogueService>();
builder.Services.AddScoped<IRentalService, RentalService>();
#endregion


#region Auth
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<CarRentalDbContext>();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
