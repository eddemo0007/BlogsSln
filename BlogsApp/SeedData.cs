using BlogsApp.Data;
using BlogsApp.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsApp
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string userPwd)
        {
            using(var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var adminId = await EnsureUser(serviceProvider, userPwd, "admin@blogapp.com", "Administrator", "Administrator");
                await EnsureRole(serviceProvider, adminId, "Administrator");

                var writerId = await EnsureUser(serviceProvider, userPwd, "writer@blogapp.com", "User", "Writer");
                await EnsureRole(serviceProvider, writerId, "Writer");

                var editorId = await EnsureUser(serviceProvider, userPwd, "editor@blogapp.com", "User", "Editor");
                await EnsureRole(serviceProvider, editorId, "Editor");

                SeedDb(context, writerId);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider, string userPwd, string userName, string name, string lastName)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync(userName);
            if(user == null)
            {
                user = new ApplicationUser
                {
                    Name = name,
                    LastName = lastName,
                    UserName = userName,
                    EmailConfirmed = true,
                    Email = userName
                };
                await userManager.CreateAsync(user, userPwd);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async  Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, string role)
        {
            IdentityResult ir;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if(roleManager == null)
            {
                throw new Exception("roleManager is null");
            }

            if(!await roleManager.RoleExistsAsync(role))
            {
                ir = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The user passsword was probably not strong enough!");
            }

            ir = await userManager.AddToRoleAsync(user, role);

            return ir;
        }

        public static void SeedDb(ApplicationDbContext context, string userId)
        {
            if (context.Posts.Any())
            {
                return;
            }

            try
            {
                context.Posts.AddRange(
                    new Post
                    {
                        Title = "Test Post 1",
                        Content = "This is the content of th post...",
                        OwnerId = userId,
                        Status = 1 //Pending
                    },
                    new Post
                    {
                        Title = "Test Post 2",
                        Content = "This is the content of th post...",
                        PublishDate = DateTime.UtcNow,
                        OwnerId = userId,
                        Status = 2, //Published
                        Comments = new List<Comment>
                        {
                            new Comment
                            {
                                Content = "Content of comment 1",
                                PublishDate = DateTime.UtcNow
                            },
                            new Comment
                            {
                                Content = "Content of comment 2",
                                PublishDate = DateTime.UtcNow
                            }
                        }
                    },
                    new Post
                    {
                        Title = "Test Post 3",
                        Content = "This is the content of th post...",
                        OwnerId = userId,
                        Status = 3 //Rejected
                    },
                    new Post
                    {
                        Title = "Test Post 4",
                        Content = "This is the content of th post...",
                        OwnerId = userId,
                        Status = 4 //Created
                    }
                );

                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}
