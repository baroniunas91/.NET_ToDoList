using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Models;

namespace ToDoList.RestfulAPI.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, EmailAddress = "admin@test.lt", Password = "labasrytas123", Role = "admin" },
                new User() { Id = 2, EmailAddress = "user1@test.lt", Password = "labasrytas123", Role = "user" },
                new User() { Id = 3, EmailAddress = "user2@test.lt", Password = "labasrytas123", Role = "user" }
            );

            modelBuilder.Entity<Todo>().HasData(
                new Todo() { Id = 1, Title = "Wash dishes", IsDone = false, UserId = 2 },
                new Todo() { Id = 2, Title = "Clean table", IsDone = false, UserId = 2 },
                new Todo() { Id = 3, Title = "Wash car", IsDone = false, UserId = 2 },
                new Todo() { Id = 4, Title = "Do homework", IsDone = false, UserId = 3 },
                new Todo() { Id = 5, Title = "Go to the gym", IsDone = false, UserId = 3 },
                new Todo() { Id = 6, Title = "Write a book", IsDone = false, UserId = 3 }
            );
        }
    }
}
