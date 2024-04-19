namespace FoodBooking.Data.Entities
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ImageId { get; set; }
<<<<<<< HEAD
        public DateTime CreateDate { get; set; }
        public ICollection<Product> Products { get; set; }
        public Image Image { get; set; }
=======
        public Image Image { get; set; }
        public ICollection<Product> Products { get; set; }

>>>>>>> ab94a4c0e989d082633e9a8cfbcdffc03b866fd3
    }
}
