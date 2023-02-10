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

        public DbSet<DeliveryDateTimeSlot> DeliveryDateTimeSlots { get; set; } = null!;

        public DbSet<DeliveryDate> DeliveryDates { get; set; } = null!;

        public DbSet<TimeSlot> TimeSlots { get; set; } = null!;

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
            modelBuilder.Entity<DeliveryDate>()
            .HasMany(c => c.TimeSlots)
            .WithMany(s => s.DeliveryDates)
            .UsingEntity<DeliveryDateTimeSlot>(j => j
                .HasOne(pt => pt.TimeSlot)
                .WithMany(p => p.DeliverуDateTimeSlots)
                .HasForeignKey(pt => pt.TimeSlotId),
               j => j
                .HasOne(pt => pt.DeliveryDate)
                .WithMany(t => t.DeliverуDateTimeSlots)
                .HasForeignKey(pt => pt.DeliveryDateId),
            j =>
            {
                j.Property(pt => pt.MaximumOrders);
                j.Property(pt => pt.MadeOrders).HasDefaultValue(0);
                j.HasKey(t => new { t.DeliveryDateId, t.TimeSlotId });
                j.ToTable("DeliverуDateTimeSlots");
            });
        }
    }
}
