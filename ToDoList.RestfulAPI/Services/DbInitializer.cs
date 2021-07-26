using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using ToDoList.RestfulAPI.Data;
using ToDoList.RestfulAPI.Interfaces;
using ToDoList.RestfulAPI.Models;

namespace ToDoList.RestfulAPI.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly string _password;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _password = BCrypt.Net.BCrypt.HashPassword("labasrytas123");
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<MyDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<MyDbContext>())
                {
                    //seed admin and users
                    if (!context.Users.Any())
                    {
                        var password = BCrypt.Net.BCrypt.HashPassword("labasrytas123");
                        var adminUser = new User
                        {
                            Id = 1,
                            EmailAddress = "admin@test.lt",
                            Password = _password,
                            Role = "admin"
                        };
                        context.Users.Add(adminUser);
                        var user1 = new User
                        {
                            Id = 2,
                            EmailAddress = "user1@test.lt",
                            Password = _password,
                            Role = "user"
                        };
                        context.Users.Add(user1);
                        var user2 = new User
                        {
                            Id = 3,
                            EmailAddress = "user2@test.lt",
                            Password = _password,
                            Role = "user"
                        };
                        context.Users.Add(user2);
                    }
                    context.SaveChanges();
                    //seed todos
                    if (!context.Todos.Any())
                    {
                        var todo1 = new Todo
                        {
                            Id = 1,
                            Title = "Wash dishes",
                            IsDone = false,
                            UserId = 2
                        };
                        context.Todos.Add(todo1);
                        var todo2 = new Todo
                        {
                            Id = 2,
                            Title = "Clean table",
                            IsDone = false,
                            UserId = 2
                        };
                        context.Todos.Add(todo2);
                        var todo3 = new Todo
                        {
                            Id = 3,
                            Title = "Wash car",
                            IsDone = false,
                            UserId = 2
                        };
                        context.Todos.Add(todo3);
                        var todo4 = new Todo
                        {
                            Id = 4,
                            Title = "Do homework",
                            IsDone = false,
                            UserId = 3
                        };
                        context.Todos.Add(todo4);
                        var todo5 = new Todo
                        {
                            Id = 5,
                            Title = "Go to the gym",
                            IsDone = false,
                            UserId = 3
                        };
                        context.Todos.Add(todo5);
                        var todo6 = new Todo
                        {
                            Id = 6,
                            Title = "Write a book",
                            IsDone = false,
                            UserId = 3
                        };
                        context.Todos.Add(todo6);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
