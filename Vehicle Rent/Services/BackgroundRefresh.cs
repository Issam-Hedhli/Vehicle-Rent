using Vehicle_Rent.Services.VehicleRent;

namespace Vehicle_Rent.Services
{
    public class BackgroundRefresh : BackgroundService
    {
        private readonly IServiceProvider _services;

        public BackgroundRefresh(IServiceProvider services)
        {
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _services.CreateScope())
                {
                    var scopedService = scope.ServiceProvider.GetRequiredService<IRentalService>();
                    await scopedService.UpdateVehicleCopies();
                    Console.WriteLine("ziw");
                }
                await Task.Delay(TimeSpan.FromHours(2), stoppingToken);
            }
        }
    }
}
