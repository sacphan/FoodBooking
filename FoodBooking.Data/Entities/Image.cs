namespace FoodBooking.Data.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public Product Product { get; set; }
        public Restaurant Restaurant { get; set; }

    }

}
