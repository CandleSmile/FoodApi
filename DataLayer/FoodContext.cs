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
        public FoodContext(DbContextOptions<FoodContext> options)
            : base(options)
        {
           // Database.EnsureDeleted();
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            var (passwordSalt, passwordHash) = HashHelper.CreatePasswordHash("14121980an");
            modelBuilder.Entity<User>().HasData(
                    new User { Id = 1, Username = "lizaveta.razumovich@gmail.com", PasswordSalt = passwordSalt, PasswordHash = passwordHash }
            ); 
        }
       
    }
}
