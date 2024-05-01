using FoodBooking.Features.Restaurants.Queries;

namespace FoodBooking.Features.Restaurants.Dto
{
    public class RestaurantDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ImageDto? Image { get; set; }
        public string? Title { get; set; }
        public DateTime CreateDate { get; set; }
        public string? Address { get; set; }
        public string? LinkCrawl { get; set; }
        public int SourceCrawlId { get; set; }
        public List<ProductDto>? Products { get; set; }

    }
}
