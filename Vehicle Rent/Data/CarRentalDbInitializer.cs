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
					new Company() { Id = "5", Name = "Nissan" },
					new Company() { Id = "6", Name = "Audi" },
					new Company() { Id = "7", Name = "Mercedes-Benz" }
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
					new VModel() { Id = "5", Name = "Altima" },
                    new VModel() { Id = "6", Name = "Auris" }, 
					new VModel() { Id = "7", Name = "Mustang" },
					new VModel() { Id = "8", Name = "Camaro" },
					new VModel() { Id = "9", Name = "Galaxy" } 
                });
					await context.SaveChangesAsync();
				}
                #endregion

                #region Vehicle

                if (!context.Vehicles.Any())
                {
                    #region Photos
                    string image1 = "https://th.bing.com/th/id/R.2290c0734b0e128758268904b0354ef3?rik=99EUYUkZEODAqg&pid=ImgRaw&r=0";
                    string image2 = "https://www.groovecar.com/media/stock/images/stills/2016/honda/accord/sport-4dr-sedan-6m/2016-honda-accord-sport-4dr-sedan-6m-046-large.jpg";
                    string image3 = "https://www.pixelstalk.net/wp-content/uploads/2016/10/Ford-F150-Wallpapers.jpg";
                    string image4 = "https://th.bing.com/th/id/R.b2c40dd226ad91d6f4456bd7e92ed69d?rik=GhxTgSGw%2frJ1yQ&riu=http%3a%2f%2fs1.cdn.autoevolution.com%2fimages%2fgallery%2fCHEVROLET-Silverado-3500-HD-Crew-Cab-5138_4.jpg&ehk=eEeQV5SXEUHT9XSwo5iO2Uj%2fE8Txi%2bBEhTrITHLzobc%3d&risl=&pid=ImgRaw&r=0";
                    string image5 = "https://i.ytimg.com/vi/bkqdyBtJh2I/maxresdefault.jpg";
                    string image6 = "https://mediacloud.carbuyer.co.uk/image/private/s--OFG81nLs--/v1579612783/carbuyer/toyota_auris_hatchback_01.jpg";
                    string image7 = "https://www.hdcarwallpapers.com/walls/ford_mustang_shelby_gt350_r-HD.jpg";
                    string image8 = "https://www.perfectautocollection.com/imagetag/85/main/l/Used-2017-Mercedes-Benz-CLA-CLA-250-4MATIC.jpg";
                    string image9 = "https://wallpapercave.com/wp/wp4286548.jpg";
                    #endregion

                    context.Vehicles.AddRange(new List<Vehicle>()
                {
                    new Vehicle() { Id = "1", Name="Toyota Camry", Description="A reliable and spacious sedan known for its comfortable ride and fuel efficiency. With a sleek exterior design and a reputation for longevity, the Camry offers a practical and enjoyable driving experience suitable for various needs, from daily commuting to family road trips.", Company = context.Companies.FirstOrDefault(c => c.Id == "1") , VModel = context.VModels.FirstOrDefault(m => m.Id == "1") , Photo = image1},
                    new Vehicle() { Id = "2", Name="Honda Accord", Description="A versatile and stylish sedan renowned for its refined performance and advanced technology features. With its spacious interior, smooth handling, and reputation for reliability, the Accord offers a comfortable and enjoyable driving experience for both daily commutes and long journeys.\r\n\r\n\r\n\r\n\r\n\r\n\r\n", Company = context.Companies.FirstOrDefault(c => c.Id == "2") , VModel = context.VModels.FirstOrDefault(m => m.Id == "2") , Photo = image2 },
                    new Vehicle() { Id = "3", Name="Ford F-150", Description="An iconic pickup truck celebrated for its rugged durability and impressive towing capabilities. Renowned for its versatility, it seamlessly transitions between workhorse duties and family adventures. With its powerful engine options and innovative features, the F-150 remains a top choice for those seeking strength, reliability, and versatility in a truck.", Company = context.Companies.FirstOrDefault(c => c.Id == "3") ,VModel = context.VModels.FirstOrDefault(m => m.Id == "3") , Photo = image3},
                    new Vehicle() { Id = "4", Name="Chevrolet Silverado", Description="A robust and dependable pickup truck known for its impressive towing capacity and rugged performance. With a spacious interior and a variety of trim options, it caters to diverse needs, from everyday work tasks to off-road adventures. Renowned for its durability and advanced technology features, the Silverado offers a combination of strength and comfort that makes it a popular choice among truck enthusiasts.", Company = context.Companies.FirstOrDefault(c => c.Id == "4") ,VModel = context.VModels.FirstOrDefault(m => m.Id == "4") , Photo = image4 },
                    new Vehicle() { Id = "5", Name="Nissan Altima", Description="A sleek and efficient sedan offering a balance of comfort, performance, and technology. With its modern design and fuel-efficient engine options, it provides a smooth and enjoyable driving experience. Renowned for its reliability and advanced safety features, the Altima is an excellent choice for those seeking a stylish and practical vehicle for daily commuting or long-distance travel.", Company = context.Companies.FirstOrDefault(c => c.Id == "5") ,VModel = context.VModels.FirstOrDefault(m => m.Id == "5") , Photo = image5},
                    new Vehicle() { Id = "6", Name="Toyota Auris", Description = "An innovative electric compact car designed for the eco-conscious urban driver. With its sleek and aerodynamic design, the Civic combines efficiency with style. Its electric motor provides instant torque and smooth acceleration, making every drive effortless and enjoyable. ", Company = context.Companies.FirstOrDefault(c => c.Id == "1") ,VModel = context.VModels.FirstOrDefault(m => m.Id == "6") , Photo = image6 },
                    new Vehicle() { Id = "7", Name="Audi Mustang", Description = "An iconic sports coupe that embodies the spirit of American muscle cars. With its muscular stance, aggressive styling, and powerful engine options, the Mustang commands attention on the road. Its refined interior features premium materials and advanced technology, ensuring both comfort and performance. ", Company = context.Companies.FirstOrDefault(c => c.Id == "6") ,VModel = context.VModels.FirstOrDefault(m => m.Id == "7") , Photo = image7 },
                    new Vehicle() { Id = "8", Name="Mercedes-Benz Camaro", Description = "A luxury convertible that combines elegance with performance. Its sleek lines and aerodynamic profile exude sophistication, while its powerful engine options deliver exhilarating performance on demand. The Camaro's luxurious interior features premium materials and state-of-the-art technology, creating a refined driving environment.", Company = context.Companies.FirstOrDefault(c => c.Id == "7") ,VModel = context.VModels.FirstOrDefault(m => m.Id == "8") , Photo = image8 },
                    new Vehicle() { Id = "9", Name="Ford Galaxy", Description = "A crossover SUV that redefines versatility and luxury in the automotive world. Combining BMW's renowned engineering prowess with the ruggedness of a capable SUV, the Rogue offers a driving experience that is both refined and adventurous. Its bold exterior design commands attention on the road, while its spacious and meticulously crafted interior provides comfort and convenience for both driver and passengers.", Company = context.Companies.FirstOrDefault(c => c.Id == "3") ,VModel = context.VModels.FirstOrDefault(m => m.Id == "9") , Photo = image9 }
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
                        for (int j = 1; j < 6; j++)
                        {
                            k++;
                            var vehicle = context.Vehicles.FirstOrDefault(v => v.Id == i.ToString());
                            if (vehicle != null)
                            {
                                
                                int mileage = 5000 * j;  
                                int year = 2015 + j;      
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

                // Fonction pour calculer le prix de location en fonction du kilométrage et de l'année
                int CalculateRentalPrice(int mileage, int year)
                {
                    
                    int prixBase = 50;   
                    int tauxKilometrage = 5; 
                    int tauxAge = 2;   

                    int fraisKilometrage = mileage * tauxKilometrage;
                    int fraisAge = (DateTime.Now.Year - year) * tauxAge;

                    int totalRentalPrice = prixBase + fraisKilometrage + fraisAge;

                    totalRentalPrice /= 1000;

                    return totalRentalPrice;
                }

                #region Unavailability
                if (!context.Unavailabilities.Any())
                {
                    var vehicleCopy1 = context.Set<VehicleCopy>().FirstOrDefault(vc => vc.Id == "1");
                    var vehicleCopy2 = context.Set<VehicleCopy>().FirstOrDefault(vc => vc.Id == "2");
                    var vehicleCopy3 = context.Set<VehicleCopy>().FirstOrDefault(vc => vc.Id == "3");
                    var vehicleCopy4 = context.Set<VehicleCopy>().FirstOrDefault(vc => vc.Id == "4");
                    var vehicleCopy5 = context.Set<VehicleCopy>().FirstOrDefault(vc => vc.Id == "5");
                    var vehicleCopy6 = context.Set<VehicleCopy>().FirstOrDefault(vc => vc.Id == "6");
                    context.Unavailabilities.AddRange(new List<Unavailability>()
                    {
                        new Unavailability() { Id="2", startDate= DateTime.Now.AddDays(1), endDate = DateTime.Now.AddDays(3),VehicleCopy=vehicleCopy1 },
                        new Unavailability() { Id="3", startDate= DateTime.Now.AddDays(5), endDate = DateTime.Now.AddDays(8),VehicleCopy=vehicleCopy1 },
                        new Unavailability() { Id="4", startDate= DateTime.Now.AddDays(10), endDate = DateTime.Now.AddDays(12),VehicleCopy = vehicleCopy2 },
                        new Unavailability() { Id="5", startDate= DateTime.Now.AddDays(15), endDate = DateTime.Now.AddDays(17),VehicleCopy=vehicleCopy3 },
                        new Unavailability() { Id="1", startDate= DateTime.Now.AddDays(10), endDate = DateTime.Now.AddDays(10),VehicleCopy = vehicleCopy5 },
                        new Unavailability() { Id="6", startDate= DateTime.Now.AddDays(15), endDate = DateTime.Now.AddDays(1),VehicleCopy=vehicleCopy6 }

                    });
                    context.SaveChanges();
                }
                #endregion

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
                    var vehicleCopy4 = context.VehicleCopies.FirstOrDefault(vc => vc.Id == "4");
                    var vehicleCopy5 = context.VehicleCopies.FirstOrDefault(vc => vc.Id == "5");
                    var vehicleCopy6 = context.VehicleCopies.FirstOrDefault(vc => vc.Id == "6");
                    var customer1 = context.Users.FirstOrDefault(r => r.Id == "1");
					var customer2 = context.Users.FirstOrDefault(r => r.Id == "2");
                    var customer3 = context.Users.FirstOrDefault(r => r.Id == "3");
                    var customer4 = context.Users.FirstOrDefault(r => r.Id == "4");
                    var customer5 = context.Users.FirstOrDefault(r => r.Id == "5");
                    var borrowedStatus = context.AvailibilityStatuses.FirstOrDefault(a => a.Id == "1");
					var returnedStatus = context.AvailibilityStatuses.FirstOrDefault(a => a.Id == "2");

                    var rating1 = new Rating()
                    {
                        Value = 4,
                        Comment = "Good service, Will rent again"
                    };
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

                    var rating5 = new Rating()
                    {
                        Value = 4,
                        Comment = "Really good, would recommend to others"
                    };
                    var rating6 = new Rating()
                    {
                        Value = 5,
                        Comment = "best price-quality ratio"
                    };

                    
                    context.RentalItems.AddRange(new List<RentalItem>()
					{
						new RentalItem { Id = "1", VehicleCopy = vehicleCopy1, Status=  borrowedStatus, User = customer2 }, 
						new RentalItem { Id = "2", VehicleCopy = vehicleCopy2, Status = returnedStatus, User = customer2 ,Ratings=rating2},
						new RentalItem { Id = "3", VehicleCopy = vehicleCopy3, Status = borrowedStatus, User = customer1 },
						new RentalItem { Id = "4", VehicleCopy = vehicleCopy1, Status = returnedStatus, User = customer1 ,Ratings = rating4},
                        new RentalItem { Id = "5", VehicleCopy = vehicleCopy3, Status = borrowedStatus, User = customer3 },
                        new RentalItem { Id = "6", VehicleCopy = vehicleCopy1, Status = returnedStatus, User = customer3 ,Ratings = rating1 },
                        new RentalItem { Id = "7", VehicleCopy = vehicleCopy3, Status = borrowedStatus, User = customer4 },
                        new RentalItem { Id = "8", VehicleCopy = vehicleCopy1, Status = returnedStatus, User = customer4 ,Ratings = rating5},
                        new RentalItem { Id = "7", VehicleCopy = vehicleCopy3, Status = borrowedStatus, User = customer5 },
                        new RentalItem { Id = "8", VehicleCopy = vehicleCopy1, Status = returnedStatus, User = customer5 ,Ratings = rating6},
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
