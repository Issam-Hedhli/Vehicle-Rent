using Microsoft.EntityFrameworkCore;
using Vehicle_Rent.Models;

namespace Vehicle_Rent.Data
{
    public class CarRentalDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Photo> Photos { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.User)
                .WithMany(u => u.Rentals)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Vehicle)
                .WithMany(v => v.Rentals)
                .HasForeignKey(r => r.VehicleId);


            base.OnModelCreating(modelBuilder);
        }
    }
    
}
