using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vehicle_Rent.Models;

namespace Vehicle_Rent.Data
{
    public class CarRentalDbContext : IdentityDbContext<User>
    {
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : base(options)
        {
        }

		public DbSet<Company> Companies { get; set; }
		public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleCopy> VehicleCopies { get; set; }
        public DbSet<RentalItem> RentalItems { get; set; }
        public DbSet<Unavailability> Unavailabilities { get; set; }
        public DbSet<AvailibilityStatus> AvailibilityStatuses { get; set; }
        public DbSet<VModel> VModels { get; set; }
        public DbSet<Rating> Ratings { get; set; }
		public override DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<RentalItem>()
            .HasOne(r => r.User)
            .WithMany(u => u.Rentals)
            .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<RentalItem>()
            .HasOne(r => r.VehicleCopy)
            .WithMany(v => v.RentalItems)
            .HasForeignKey(r => r.VehicleCopyId);

			modelBuilder.Entity<VehicleCopy>()
            .HasOne(b => b.Vehicle)
            .WithMany(b => b.VehicleCopies)
            .HasForeignKey(b => b.IdVehicle);

            modelBuilder.Entity<RentalItem>()
                .HasOne(ri => ri.Status)
                .WithMany(s => s.RentalItems);

			modelBuilder.Entity<Vehicle>()
			.HasOne(v => v.Company) 
			.WithMany(c => c.Vehicles) 
			.HasForeignKey(v => v.CompanyId);

			modelBuilder.Entity<Vehicle>()
	        .HasOne(v => v.VModel) 
	        .WithMany(vm => vm.Vehicles) 
	        .HasForeignKey(v => v.VModelId); 

			modelBuilder.Entity<RentalItem>()
	        .HasOne(r => r.Ratings)
	        .WithOne(v => v.RentalItem)
	        .HasForeignKey<RentalItem>(ri => ri.RatingId);

            modelBuilder.Entity<Unavailability>()
                .HasOne(u => u.VehicleCopy)
                .WithMany(vc => vc.Unavailabilities)
                .HasForeignKey(u => u.vehicleCopyId);

            base.OnModelCreating(modelBuilder);
        }
    }
    
}
