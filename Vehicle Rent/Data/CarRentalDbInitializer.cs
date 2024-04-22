using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using Vehicle_Rent.Models;
using static System.Net.WebRequestMethods;


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
					new VModel() { Id = "4", Name = "Silverado" },
					new VModel() { Id = "5", Name = "Altima" }
				});
					await context.SaveChangesAsync();
				}
				#endregion

				#region Vehicle

				if (!context.Vehicles.Any())
				{
                    #region Photos
                    string image1 = "https://th.bing.com/th/id/R.2290c0734b0e128758268904b0354ef3?rik=99EUYUkZEODAqg&pid=ImgRaw&r=0";
                    string image2 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFiaQZbDwtZQxmOw2E-0MOjtZPUDEmroPv9w&s";
                    string image3 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTQD5QwhxumoqAQEYazQweYLwgWeQtee2_KcA&s";
                    string image4 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRFJAZK2UkDXFB9rJ6vW6BVaGv1mcAgr2m_7Q&s";
                    string image5 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQKz4vaMQU8Xqc8fw0hd1JKKARJPWoTJFyikw&s";
                    #endregion

                    context.Vehicles.AddRange(new List<Vehicle>()
				{
					new Vehicle() { Id = "1", Name="Toyota Camry", Description="A reliable and spacious sedan known for its comfortable ride and fuel efficiency. With a sleek exterior design and a reputation for longevity, the Camry offers a practical and enjoyable driving experience suitable for various needs, from daily commuting to family road trips.", Company = context.Companies.FirstOrDefault(c => c.Id == "1") , VModel = context.VModels.FirstOrDefault(m => m.Id == "1") , Photo = image1, IsAvailable = true},
					new Vehicle() { Id = "2", Name="Honda Accord", Description="A versatile and stylish sedan renowned for its refined performance and advanced technology features. With its spacious interior, smooth handling, and reputation for reliability, the Accord offers a comfortable and enjoyable driving experience for both daily commutes and long journeys.\r\n\r\n\r\n\r\n\r\n\r\n\r\n", Company = context.Companies.FirstOrDefault(c => c.Id == "2") , VModel = context.VModels.FirstOrDefault(m => m.Id == "2") , Photo = image2 , IsAvailable = true},
					new Vehicle() { Id = "3", Name="Ford F-150", Description="An iconic pickup truck celebrated for its rugged durability and impressive towing capabilities. Renowned for its versatility, it seamlessly transitions between workhorse duties and family adventures. With its powerful engine options and innovative features, the F-150 remains a top choice for those seeking strength, reliability, and versatility in a truck.", Company = context.Companies.FirstOrDefault(c => c.Id == "3") ,VModel = context.VModels.FirstOrDefault(m => m.Id == "3") , Photo = image3, IsAvailable = true},
					new Vehicle() { Id = "4", Name="Chevrolet Silverado", Description="A robust and dependable pickup truck known for its impressive towing capacity and rugged performance. With a spacious interior and a variety of trim options, it caters to diverse needs, from everyday work tasks to off-road adventures. Renowned for its durability and advanced technology features, the Silverado offers a combination of strength and comfort that makes it a popular choice among truck enthusiasts.", Company = context.Companies.FirstOrDefault(c => c.Id == "4") ,VModel = context.VModels.FirstOrDefault(m => m.Id == "4") , Photo = image4 , IsAvailable = true},
					new Vehicle() { Id = "5", Name="Nissan Altima", Description="A sleek and efficient sedan offering a balance of comfort, performance, and technology. With its modern design and fuel-efficient engine options, it provides a smooth and enjoyable driving experience. Renowned for its reliability and advanced safety features, the Altima is an excellent choice for those seeking a stylish and practical vehicle for daily commuting or long-distance travel.", Company = context.Companies.FirstOrDefault(c => c.Id == "5") ,VModel = context.VModels.FirstOrDefault(m => m.Id == "5") , Photo = image5 , IsAvailable = true }
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
                            var vehicle = context.Vehicles.FirstOrDefault(v => v.Id == i.ToString());
                            if (vehicle != null)
                            {
                                // Calculate rental price based on mileage and year
                                int mileage = 5000 * i;   // Example mileage calculation
                                int year = 2020 + i;      // Example year calculation
                                int rentalPrice = CalculateRentalPrice(mileage, year);

                                vehicleCopies.Add(new VehicleCopy()
                                {
                                    Id = k.ToString(),
                                    Vehicle = vehicle,
                                    RentalPrice = rentalPrice,
                                    Mileage = mileage,
                                    Year = year
                                });
                            }
                        }
                    }
                    context.VehicleCopies.AddRange(vehicleCopies);
                    await context.SaveChangesAsync();
                }
                #endregion

                // Function to calculate rental price based on mileage and year
                int CalculateRentalPrice(int mileage, int year)
                {
                    // Example calculation logic
                    int basePrice = 50;   // Base rental price
                    int mileageRate = 2; // Rate per mile (in cents)
                    int ageRate = 10;   // Rate per year of age (in cents)

                    // Calculate additional charges based on mileage and year
                    int mileageCharge = mileage * mileageRate;
                    int ageCharge = (DateTime.Now.Year - year) * ageRate;

                    // Total rental price
                    int totalRentalPrice = basePrice + mileageCharge + ageCharge;

                    return totalRentalPrice;
                }




                #region availabilityStatus
                if (!context.AvailibilityStatuses.Any())
				{
					context.AvailibilityStatuses.AddRange( new List<AvailibilityStatus>() 
					{
						new AvailibilityStatus() {Id="1",name="Borrowed"},
						new AvailibilityStatus() {Id="2",name="Returned"}
					});
					context.SaveChanges();
				}
				#endregion

                #region RentalItem

                if (!context.RentalItems.Any())
				{
					var vehicleCopy1 = context.VehicleCopies.FirstOrDefault(vc => vc.Id == "1");
					var vehicleCopy2 = context.VehicleCopies.FirstOrDefault(vc => vc.Id == "2");
					var vehicleCopy3 = context.VehicleCopies.FirstOrDefault(vc => vc.Id == "3");
					var customer1 = context.Users.FirstOrDefault(r => r.Id == "1");
					var customer2 = context.Users.FirstOrDefault(r => r.Id == "2"); 
					var customer3 = context.Users.FirstOrDefault(r => r.Id == "3");
					var borrowedStatus = context.AvailibilityStatuses.FirstOrDefault(a => a.Id == "1");
					var returnedStatus = context.AvailibilityStatuses.FirstOrDefault(a => a.Id == "2");
					var rating2 = new Rating()
					{
						Value = 5,
						Comment = "Enjoyed its rides"
					};
					var rating4 = new Rating()
					{
						Value = 1,
						Comment = "didn't really like it"
					};

                    context.RentalItems.AddRange(new List<RentalItem>()
					{
						new RentalItem { Id = "1", VehicleCopy = vehicleCopy1, Status=  borrowedStatus, User = customer2 }, 
						new RentalItem { Id = "2", VehicleCopy = vehicleCopy2, Status = returnedStatus, User = customer3 ,Ratings=rating2},
						new RentalItem { Id = "3", VehicleCopy = vehicleCopy3, Status = borrowedStatus, User = customer1 },
						new RentalItem { Id = "4", VehicleCopy = vehicleCopy1, Status = returnedStatus, User = customer1 ,Ratings = rating4}
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

				var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

				#region Users
				// First Customer
				string customerEmail = "customer@carrental.com";
				var customer = await userManager.FindByEmailAsync(customerEmail);
				if (customer == null)
				{
					User newCustomer = new User { Id = "1", UserName = customerEmail, Email = customerEmail,Name="Sami", Image= "https://scontent.ftun5-1.fna.fbcdn.net/v/t1.6435-9/120645766_3263034770470903_6610932504759370937_n.jpg?_nc_cat=105&ccb=1-7&_nc_sid=5f2048&_nc_ohc=zfsHXV9ToLYAb6cQ9ho&_nc_ht=scontent.ftun5-1.fna&oh=00_AfAr5QEPUSx6fbsyFmRhKf1a4WBFnugVku-lt_fJN-M05A&oe=664BB337" };
					await userManager.CreateAsync(newCustomer, "Customer123!"); 
					await userManager.AddToRoleAsync(newCustomer, UserRoles.Customer);
				}

				// Second Customer
				string secondaryCustomerEmail = "secondCustomer@carrental.com";
				var secondaryCustomer = await userManager.FindByEmailAsync(secondaryCustomerEmail);
                if (secondaryCustomer == null)
                {
					User newCustomer = new User { Id = "2", UserName = secondaryCustomerEmail, Email = secondaryCustomerEmail,Name="Issam",Image= "https://scontent.ftun5-1.fna.fbcdn.net/v/t39.30808-6/289057250_4012267192331843_3921978455873349113_n.jpg?_nc_cat=101&ccb=1-7&_nc_sid=5f2048&_nc_ohc=l5mTzPaYOg8Ab69hAbh&_nc_ht=scontent.ftun5-1.fna&oh=00_AfD8S_CuzLyQ1IAeCTx2bqsqm-rTakYe_u7pc3D4L2D-nA&oe=662A2AA0" };
					await userManager.CreateAsync(newCustomer, "Customer123!");
					await userManager.AddToRoleAsync(newCustomer, UserRoles.Customer);
				}
				#endregion
			}
		}

	}
}
