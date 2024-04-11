using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vehicle_Rent.Models;

namespace Vehicle_Rent.Data
{
    public class CarRentalDbContext : IdentityDbContext<IdentityUser>
    {
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : base(options)
        {
        }

		public DbSet<Company> Companies { get; set; }
		public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleCopy> VehicleCopies { get; set; }
        public DbSet<RentalItem> RentalItems { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<AvailibilityStatusWrapper> AvailibilityStatuses { get; set; }
        public DbSet<VModel> VModels { get; set; }
        public DbSet<Rating> Ratings { get; set; }
		public DbSet<User> Users { get; set; }

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
            .Property(v => v.Status)
            .HasConversion<int>();

            modelBuilder.Entity<Vehicle>()
            .HasMany(v => v.Photos)
            .WithOne(p => p.Vehicle)
            .HasForeignKey(p => p.VehicleId);

			modelBuilder.Entity<Vehicle>()
			.HasOne(v => v.Company) 
			.WithMany(c => c.Vehicles) 
			.HasForeignKey(v => v.CompanyId);

			modelBuilder.Entity<Vehicle>()
	        .HasOne(v => v.VModel) 
	        .WithMany(vm => vm.Vehicles) 
	        .HasForeignKey(v => v.VModelId); 

			modelBuilder.Entity<Rating>()
	        .HasOne(r => r.RentalItem)
	        .WithMany(v => v.Ratings)
	        .HasForeignKey(r => r.RentalId);



			base.OnModelCreating(modelBuilder);
        }
    }
    
}
