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
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
    }
}
