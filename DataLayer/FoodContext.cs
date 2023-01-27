using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Helpers;


namespace DataLayer
{
    using DataLayer.Items;
    using Microsoft.EntityFrameworkCore;
    
    public class FoodContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Meal> Meals { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Ingredient> Ingredients { get; set; } = null!;
        public DbSet<Area> Areas { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;

        public FoodContext(DbContextOptions<FoodContext> options)
            : base(options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meal>()
              .HasMany(c => c.Ingredients)
              .WithMany(s => s.Meals)
              .UsingEntity(j => j.ToTable("MealIngredients"));
            modelBuilder.Entity<Meal>()
              .HasMany(c => c.Tags)
              .WithMany(s => s.Meals)
              .UsingEntity(j => j.ToTable("MealTags"));


            var (passwordSalt, passwordHash) = HashHelper.CreatePasswordHash("14121980an");
         
            modelBuilder.Entity<User>().HasData(
                    new User { Id = 1, Username = "lizaveta.razumovich@gmail.com", PasswordSalt = passwordSalt, PasswordHash = passwordHash }
            ); 
        }
       
    }
}
