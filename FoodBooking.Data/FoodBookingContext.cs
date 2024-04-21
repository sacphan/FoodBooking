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
                string conn = "Server=.;Database=FoodBooking;TrustServerCertificate=True;Trusted_Connection=true";

                builder.UseSqlServer(conn);
            }

            base.OnConfiguring(builder);
        }

        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Image> Images { get; set; }
    }

}
