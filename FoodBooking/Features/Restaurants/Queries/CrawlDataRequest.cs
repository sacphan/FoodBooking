using FoodBooking.Data.Entities;
using FoodBooking.Data.Models.Exceptions;
using FoodBooking.Features.Restaurants.Queries;
using FoodBooking.Reponsitory.Product;
using FoodBooking.Reponsitory.Restaurants;
using HtmlAgilityPack;
using MediatR;
using PuppeteerSharp;
using System.Net;

namespace FoodBooking.Features.Restaurants.Queries
{
    public class CrawlDataRequest : IRequest<CrawlDataReponse>
    {
        public string? sourceLink { get; set; }
    }

    public class CrawlDataReponse
    {
        public Guid RestaurantId { get; set; }
    }

    public class CrawlDataRequestRequestHandler : IRequestHandler<CrawlDataRequest, CrawlDataReponse>
    {
        private readonly IProductReponsitory _productReponsitory;
        private readonly IRestaurantRepository _restaurantRepository;

        public CrawlDataRequestRequestHandler(IProductReponsitory productReponsitory, IRestaurantRepository restaurantRepository)
        {
            _productReponsitory = productReponsitory;
            _restaurantRepository = restaurantRepository;
        }
        public async Task<CrawlDataReponse> Handle(CrawlDataRequest request, CancellationToken cancellationToken)
        {
            var result = new CrawlDataReponse();
            var html = await GetHtmlContentAsync(request.sourceLink);
            await ExtractDataFromHtml(html);
            return result;
        }

        private async Task<string> GetHtmlContentAsync(string url)
        {
            // Download the Chromium browser
            await new BrowserFetcher().DownloadAsync();

            // Launch a headless browser
            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            // Open a new page/tab
            using var page = await browser.NewPageAsync();

            // Navigate to the specified URL
            await page.GoToAsync(url);

            
            // Get the page's HTML content
            return await page.GetContentAsync();

        }

        private async Task ExtractDataFromHtml(string html)
        {
            var listProduct = new List<Product>();
            var restaurant = new Restaurant()
            {
                Image = new Image()
            };
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var restaurantImageNode = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'detail-restaurant-img')]//img");
            restaurant.Image.ImageUrl = restaurantImageNode != null ? restaurantImageNode.GetAttributeValue("src", string.Empty) : string.Empty;
            var restaurantNameNode = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'detail-restaurant-info')]//h1[contains(@class, 'name-restaurant')]");
            restaurant.Name = restaurantNameNode != null ? restaurantNameNode.InnerText.Trim() : string.Empty;
            var restaurantAddress = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'detail-restaurant-info')]//div[contains(@class, 'address-restaurant')]");
            restaurant.Address = restaurantAddress != null ? restaurantAddress.InnerText.Trim() : string.Empty;

            var productNodes = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'item-restaurant-row')]");
            foreach (var node in productNodes)
            {
                var product = new Product()
                {
                    Image = new Image()
                };

                var imageNode = node.SelectSingleNode(".//div[contains(@class, 'item-restaurant-img')]//img");
                product.Image.ImageUrl = imageNode != null ? imageNode.GetAttributeValue("src", string.Empty) : string.Empty;

                var nameNode = node.SelectSingleNode(".//div[contains(@class, 'item-restaurant-info')]//h2[contains(@class, 'item-restaurant-name')]");
                product.Name = nameNode != null ? nameNode.InnerText.Trim() : string.Empty;

                var descNode = node.SelectSingleNode(".//div[contains(@class, 'item-restaurant-info')]//div[contains(@class, 'item-restaurant-desc')]");
                product.Description = descNode != null ? descNode.InnerText.Trim() : string.Empty;

                var priceNode = node.SelectSingleNode(".//div[contains(@class, 'product-price')]//div[contains(@class, 'current-price')]");
                product.Price = priceNode != null ? double.Parse(priceNode.InnerText.Trim()) : 0;
                
                listProduct.Add(product);
            }

            _restaurantRepository.Create(restaurant);
            foreach (var product in listProduct)
            {
                product.Restaurant = restaurant;
                _productReponsitory.Create(product);
            }

            if (await _restaurantRepository.SaveChangesAsync() < 0)
            {
                throw new MediatorException(ExceptionType.Error, "Error update this restaurant");
            }


        }

    }
}
