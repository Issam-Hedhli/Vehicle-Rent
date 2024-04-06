﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using Vehicle_Rent.Models;


namespace Vehicle_Rent.Data
{
	public class CarRentalDbInitializer
	{
		public static async Task Seed(IApplicationBuilder ApplicationBuilder)
		{
			using (var serviceScope = ApplicationBuilder.ApplicationServices.CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetService<CarRentalDbContext>();
				context.Database.EnsureCreated();

				#region Company

				if (!context.Companies.Any())
				{
					context.Companies.AddRange(new List<Company>()
				{
					new Company() { Id = "1", Name = "Toyota" },
					new Company() { Id = "2", Name = "Honda" },
					new Company() { Id = "3", Name = "Ford" },
					new Company() { Id = "4", Name = "Chevrolet" },
					new Company() { Id = "5", Name = "Nissan" }
				});
					await context.SaveChangesAsync();
				}
				#endregion

				#region VehicleModel

				if (!context.VModels.Any())
				{
					context.VModels.AddRange(new List<VModel>()
				{
					new VModel() { Id = "1", Name = "Camry" },
					new VModel() { Id = "2", Name = "Accord" },
					new VModel() { Id = "3", Name = "F-150" },
					new VModel() { Id = "3", Name = "F-150" },
					new VModel() { Id = "4", Name = "Silverado" },
					new VModel() { Id = "5", Name = "Altima" }
				});
					await context.SaveChangesAsync();
				}
				#endregion

				#region Vehicle

				if (!context.Vehicles.Any())
				{
					context.Vehicles.AddRange(new List<Vehicle>()
				{
					new Vehicle() { Id = "1", Company = context.Companies.FirstOrDefault(c => c.Id == "1") , VModel = context.VModels.FirstOrDefault(m => m.Id == "1"), RentalPrice = 50.00m },
					new Vehicle() { Id = "2", Company = context.Companies.FirstOrDefault(c => c.Id == "2") , VModel = context.VModels.FirstOrDefault(m => m.Id == "2"), RentalPrice = 150.00m },
					new Vehicle() { Id = "3", Company = context.Companies.FirstOrDefault(c => c.Id == "3") ,VModel = context.VModels.FirstOrDefault(m => m.Id == "3"), RentalPrice = 250.00m },
					new Vehicle() { Id = "4", Company = context.Companies.FirstOrDefault(c => c.Id == "4") ,VModel = context.VModels.FirstOrDefault(m => m.Id == "4"), RentalPrice = 350.00m },
					new Vehicle() { Id = "5", Company = context.Companies.FirstOrDefault(c => c.Id == "5") ,VModel = context.VModels.FirstOrDefault(m => m.Id == "5"), RentalPrice = 450.00m }
				});
					await context.SaveChangesAsync();
				}
				#endregion

				#region VehicleCopy
				if (!context.VehicleCopies.Any())
				{
					List<VehicleCopy> vehicleCopies = new List<VehicleCopy>();
					int k = 0;
					for (int i = 1; i < 10; i++)
					{
						for (int j = 1; j < 3; j++)
						{
							k++;
							vehicleCopies.Add(new VehicleCopy() { Id = k.ToString(), Vehicle = context.Vehicles.FirstOrDefault(v => v.Id == i.ToString()) });
						}
					}
					context.VehicleCopies.AddRange(vehicleCopies);
					await context.SaveChangesAsync();
				}
				#endregion

				#region RentalItem

				if (!context.RentalItems.Any())
				{
					var vehicleCopy1 = context.VehicleCopies.FirstOrDefault(vc => vc.Id == "1");
					var vehicleCopy2 = context.VehicleCopies.FirstOrDefault(vc => vc.Id == "2");
					var vehicleCopy3 = context.VehicleCopies.FirstOrDefault(vc => vc.Id == "3");
					var borrowedStatus = context.AvailibilityStatuses.FirstOrDefault(s => s.Id == "1").Status; // Assuming "1" represents "borrowed" status
					var returnedStatus = context.AvailibilityStatuses.FirstOrDefault(s => s.Id == "2").Status; // Assuming "2" represents "returned" status
					var customer1 = context.Users.FirstOrDefault(r => r.Id == "1");
					var customer2 = context.Users.FirstOrDefault(r => r.Id == "2"); // Using "customer2" for consistency
					var customer3 = context.Users.FirstOrDefault(r => r.Id == "3");

					context.RentalItems.AddRange(new List<RentalItem>()
					{
						new RentalItem { Id = "1", VehicleCopy = vehicleCopy1, Status = borrowedStatus, User = customer2 }, // Using "customer" property
						new RentalItem { Id = "2", VehicleCopy = vehicleCopy2, Status = returnedStatus, User = customer3 },
						new RentalItem { Id = "3", VehicleCopy = vehicleCopy3, Status = borrowedStatus, User = customer1 },
						new RentalItem { Id = "4", VehicleCopy = vehicleCopy1, Status = returnedStatus, User = customer1 }
					});
					await context.SaveChangesAsync();
				}

				#endregion

			}
			


		}


		public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationbuilder)
		{
			using (var serviceScope = applicationbuilder.ApplicationServices.CreateScope())
			{
				var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				#region Roles
				if (!await roleManager.RoleExistsAsync(UserRoles.Customer))
				{
					await roleManager.CreateAsync(new IdentityRole(UserRoles.Customer));
				}
				#endregion

				var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

				#region Users
				// First Customer
				string customerEmail = "customer@carrental.com";
				var customer = await userManager.FindByEmailAsync(customerEmail);
				if (customer == null)
				{
					User newCustomer = new User { Id = "1", UserName = customerEmail, Email = customerEmail };
					await userManager.CreateAsync(newCustomer, "Customer123!"); 
					await userManager.AddToRoleAsync(newCustomer, UserRoles.Customer);
				}

				// Second Customer
				string secondaryCustomerEmail = "secondCustomer@carrental.com";
				var secondaryCustomer = await userManager.FindByEmailAsync(secondaryCustomerEmail);
				if (secondaryCustomer == null)
				{
					User newCustomer = new User { Id = "2", UserName = secondaryCustomerEmail, Email = secondaryCustomerEmail };
					await userManager.CreateAsync(newCustomer, "Customer123!");
					await userManager.AddToRoleAsync(newCustomer, UserRoles.Customer);
				}
				#endregion
			}
		}

	}
}