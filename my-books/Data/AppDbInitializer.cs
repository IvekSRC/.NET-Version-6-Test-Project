﻿using my_books.Auth;
using my_books.Data.Models;

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
                            Title = "1st Book itle",
                            Description = "1st Book Description",
                            isRead = true,
                            DateRead = DateTime.Now.AddDays(-10),
                            Rate = 4,
                            Genre = "Biography",
                            CoverUrl = "https...",
                            DateAdded = DateTime.Now
                        },
                        new Book()
                        {
                            Title = "2nd Book itle",
                            Description = "2nt Book Description",
                            isRead = false,
                            Genre = "Biography",
                            CoverUrl = "https...",
                            DateAdded = DateTime.Now
                        }
                     );

                    context.SaveChanges();
                }

                
            }
        }
    }
}
