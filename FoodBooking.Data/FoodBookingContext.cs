using FoodBooking.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodBooking.Data
{
    public class FoodBookingContext : DbContext
    {
        public FoodBookingContext()
        {

        }
        public FoodBookingContext(DbContextOptions<FoodBookingContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                string conn = Environment.GetEnvironmentVariable("FoodBookingConnectionString", EnvironmentVariableTarget.User);
                builder.UseSqlServer(conn);
            }

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(b => b.CreateDate)
                .IsRequired()
                .HasDefaultValueSql("getutcdate()")
                ;

            modelBuilder.Entity<Restaurant>()
                .Property(b => b.CreateDate)
                .IsRequired()
                .HasDefaultValueSql("getutcdate()");
        }

        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Image> Images { get; set; }
    }

}
