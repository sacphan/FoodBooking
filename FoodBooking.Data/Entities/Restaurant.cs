namespace FoodBooking.Data.Entities
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public Guid? ImageId { get; set; }
        public DateTime CreateDate { get; set; }
        public string LinkCrawl { get; set; }
        public int SourceCrawlId { get; set; }
        public Image Image { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
