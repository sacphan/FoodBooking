using FoodBooking.Features.Restaurants.Queries;

namespace FoodBooking.Features.Restaurants.Dto
{
    public class RestaurantDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ImageDto Image { get; set; }

    }
}
