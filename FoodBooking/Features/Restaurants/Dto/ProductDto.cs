using FoodBooking.Data.Entities;

namespace FoodBooking.Features.Restaurants.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }
        public ImageDto? Image { get; set; }
    }
}
