using Microsoft.EntityFrameworkCore;
using Vehicle_Rent.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMvc().AddRazorRuntimeCompilation();

#region DataAccess
//builder.Services.AddDbContext<CarRentalDbContext>(option => option.UseLazyLoadingProxies().UseInMemoryDatabase("RentCar"));

builder.Services.AddDbContext<CarRentalDbContext>(option => option.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
