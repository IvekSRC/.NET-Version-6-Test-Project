using Microsoft.AspNetCore.Identity;
using my_books.Auth;
using my_books.Data.Models;
using System.Security.Claims;

namespace my_books.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (context?.Books.Any() == false)
                {
                    context.Books.AddRange
                    (
                        new Book()
                        {
                            Title = "Book 1",
                            Description = "Description 1",
                            isRead = true,
                            DateRead = DateTime.Now.AddDays(-10),
                            Rate = 4,
                            Genre = "Biography",
                            CoverUrl = "Some URL 1",
                            DateAdded = DateTime.Now
                        },
                        new Book()
                        {
                            Title = "Book 2",
                            Description = "Description 2",
                            isRead = true,
                            DateRead = DateTime.Now.AddDays(-5),
                            Rate = 7,
                            Genre = "Comedy",
                            CoverUrl = "Some URL 2",
                            DateAdded = DateTime.Now
                        },
                        new Book()
                        {
                            Title = "Book 3",
                            Description = "Description 3",
                            isRead = true,
                            DateRead = DateTime.Now.AddDays(-2),
                            Rate = 6,
                            Genre = "Comedy",
                            CoverUrl = "Some URL 3",
                            DateAdded = DateTime.Now
                        }
                     );

                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedBasicUserAsync(UserManager<IdentityUser> userManager)
        {
            RegisterModel model = new RegisterModel
            {
                Username = "super-admin",
                Password = "Password@123",
                Email = "super-admin@gmail.com"
            };

            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists == null)
            {
                IdentityUser user = new()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded) { 
                    await userManager.AddToRoleAsync(user, UserRoles.SuperAdmin);
                    await userManager.AddToRoleAsync(user, UserRoles.Admin);
                    await userManager.AddToRoleAsync(user, UserRoles.User);

                    await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Users.View));
                    await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Users.Edit));
                    await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Users.Delete));
                    await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Users.Create));
                }
            }
        }
    }
}
