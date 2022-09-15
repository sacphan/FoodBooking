namespace FoodBooking.Data.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid RestaurantId { get; set; }
        public Guid? ImageId { get; set; }
        public double Price { get; set; }
        public Image Image { get; set; }
        public Restaurant Restaurant { get; set; }

    }
}
