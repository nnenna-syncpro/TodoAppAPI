using Microsoft.EntityFrameworkCore;
using TodoAppAPI.Models;

namespace TodoAppAPI.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Priority> Priorities { get; set; }

        //seed data for Categories, Statuses, and Priorities
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId="home", Title="Home"},
                new Category { CategoryId = "work", Title = "Work" },
                new Category { CategoryId = "study", Title = "Study" },
                new Category { CategoryId = "heal", Title = "Health" },
                new Category { CategoryId = "fit", Title = "Fitness" },
                new Category { CategoryId = "care", Title = "Self-Care" },
                new Category { CategoryId = "soc", Title = "Social" },
                new Category { CategoryId = "fun", Title = "Hobby" },
                new Category { CategoryId = "exec", Title = "Admin" },
                new Category { CategoryId = "shop", Title = "Shopping" }
            );

            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = "open", Title = "Open" },
                new Status { StatusId = "progress", Title = "In-Progress" },
                new Status { StatusId = "closed", Title = "Closed" }
            );

            modelBuilder.Entity<Priority>().HasData(
                new Priority { PriorityId = "low", Title = "Low" },
                new Priority { PriorityId = "medium", Title = "Medium" },
                new Priority { PriorityId = "high", Title = "High" }
            );
        }
    }
}
