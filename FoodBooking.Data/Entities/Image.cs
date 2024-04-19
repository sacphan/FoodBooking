namespace FoodBooking.Data.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
<<<<<<< HEAD
        public ICollection<Restaurant> Restaurants { get; set; }
        public ICollection<Product> Products { get; set; }
=======
        public Product Product { get; set; }
        public Restaurant Restaurant { get; set; }

>>>>>>> ab94a4c0e989d082633e9a8cfbcdffc03b866fd3
    }

}
