using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vehicle_Rent.Data;
using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Specific;
using Vehicle_Rent.Services.EmailSender;
using Vehicle_Rent.Services.Payment;
using Vehicle_Rent.Services.Profile;
using Vehicle_Rent.Services.VehicleCatalogue;
using Vehicle_Rent.Services.VehicleCopyStore;
using Vehicle_Rent.Services.VehicleRent;
using Stripe;

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
builder.Services.AddScoped<IAvailabilityStatusRepository, AvailabilityStatusRepository>();
#endregion

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

#region Service
builder.Services.AddScoped<IVehicleCatalogueService, VehicleCatalogueService>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IVehicleCopyStoreService, VehicleCopyStoreService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IProfileService, ProfileService>();
#endregion


#region AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

#region Auth
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<CarRentalDbContext>();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});
#endregion


#region Authorization
builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("User",
        authBuilder =>
        {
            authBuilder.RequireRole("Customer");
        });

});
#endregion

#region Claims
builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, ApplicationUserClaimsPrincipalFactory>();
#endregion

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));
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

CarRentalDbInitializer.SeedUsersAndRolesAsync(app).Wait();
CarRentalDbInitializer.Seed(app).Wait();


app.Run();
