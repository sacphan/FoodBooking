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

        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Product> Products { get; set; }

    }

}
