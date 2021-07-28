using Microsoft.EntityFrameworkCore;
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
    }
}
