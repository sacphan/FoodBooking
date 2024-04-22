using FoodBooking.Data.Entities;
using FoodBooking.Data.Models.Exceptions;
using FoodBooking.Features.Restaurants.Queries;
using FoodBooking.Reponsitory.Products;
using FoodBooking.Reponsitory.Restaurants;
using HtmlAgilityPack;
using MediatR;
using PuppeteerSharp;
using System.Globalization;
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
            await ExtractDataFromHtml(html, request.sourceLink);
            return result;
        }

        private async Task<string> GetHtmlContentAsync(string url)
        {
            // Download the Chromium browser
            await new BrowserFetcher().DownloadAsync();

            // Launch a headless browser
            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = false
            });

            // Open a new page/tab
            using var page = await browser.NewPageAsync();

            // Navigate to the specified URL
            await page.GoToAsync(url);
            Thread.Sleep(2000);


            // Get the page's HTML content
            return await page.GetContentAsync();

        }

        private void ExtractDataForShoppeeFood(HtmlDocument htmlDoc, List<Product> listProduct, Restaurant restaurant)
        {
            var productNodes = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'item-restaurant-row')]");

            var restaurantImageNode = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'detail-restaurant-img')]//img");
            restaurant.Image.ImageUrl = restaurantImageNode != null ? restaurantImageNode.GetAttributeValue("src", string.Empty) : string.Empty;
            var restaurantNameNode = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'detail-restaurant-info')]//h1[contains(@class, 'name-restaurant')]");
            restaurant.Name = restaurantNameNode != null ? restaurantNameNode.InnerText.Trim() : string.Empty;
            var restaurantAddress = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'detail-restaurant-info')]//div[contains(@class, 'address-restaurant')]");
            restaurant.Address = restaurantAddress != null ? restaurantAddress.InnerText.Trim() : string.Empty;

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

                var priceNode = node.SelectSingleNode(".//div[contains(@class, 'item-restaurant-more')]//div[contains(@class, 'current-price')]");
                decimal priceDecimal = decimal.Parse(priceNode.InnerText.Replace("đ", string.Empty).Trim(), NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.GetCultureInfo("vi-VN"));
                product.Price = priceDecimal;


                listProduct.Add(product);
            }
        }

        private void ExtractDataForGrabFood(HtmlDocument htmlDoc, List<Product> listProduct, Restaurant restaurant)
        {

            var restaurantNameNode = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'merchantInfo___1GGGp')]//h1[contains(@class, 'name___1Ls94')]");
            restaurant.Name = restaurantNameNode != null ? restaurantNameNode.InnerText.Trim() : string.Empty;

            var productNodes = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'menuItemWrapper___1xIAB')]");
            foreach (var node in productNodes)
            {
                var disableNode = node.SelectSingleNode(".//div[contains(@class, 'menuItem--disable___3RX8D')]");
                if (disableNode==null)
                {
                    var product = new Product()
                    {
                        Image = new Image()
                    };

                    var imageNode = node.SelectSingleNode(".//div[contains(@class, 'menuItemPhoto___1zY0s')]//img");
                    product.Image.ImageUrl = imageNode != null ? imageNode.GetAttributeValue("src", string.Empty) : string.Empty;

                    var nameNode = node.SelectSingleNode(".//div[contains(@class, 'itemNameDescription___38JZv')]//p[contains(@class, 'itemNameTitle___1sFBq')]");
                    product.Name = nameNode != null ? nameNode.InnerText.Trim() : string.Empty;

                    var descNode = node.SelectSingleNode(".//div[contains(@class, 'itemNameDescription___38JZv')]//p[contains(@class, 'itemDescription___2cIzt')]");
                    product.Description = descNode != null ? descNode.InnerText.Trim() : string.Empty;

                    var priceNode = node.SelectSingleNode(".//div[contains(@class, 'itemPrice___DqSxA')]//p[contains(@class, 'discountedPrice___3MBVA')]");
                    decimal priceDecimal = decimal.Parse(priceNode.InnerText.Trim(), NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.GetCultureInfo("vi-VN"));
                    product.Price = priceDecimal;
                    listProduct.Add(product);
                }    
            }
        }

        private async Task ExtractDataFromHtml(string html, string sourceLink)
        {
            var listProduct = new List<Product>();
            var restaurant = new Restaurant()
            {
                Image = new Image(),
                LinkCrawl = sourceLink
                
            };
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            if (sourceLink.ToLower().Contains("shopeefood"))
            {
                ExtractDataForShoppeeFood(htmlDoc, listProduct, restaurant);
            }    
            else
            {
                ExtractDataForGrabFood(htmlDoc, listProduct, restaurant);
            }

            var existRestaurant = await _restaurantRepository.FindByLinkCrawlAsync(sourceLink);
            if (existRestaurant==null)
            {
                existRestaurant = restaurant;
                _restaurantRepository.Create(restaurant);
            }
            else
            {
                existRestaurant.Name = restaurant.Name;
                existRestaurant.Image.ImageUrl = restaurant.Image.ImageUrl;
                existRestaurant.Address = restaurant.Address;
            }

            foreach (var product in listProduct)
            {
                product.Restaurant = existRestaurant;
                var existProduct = await _productReponsitory.FindByNameAndRestaurantId(product.Name, existRestaurant.Id);
                if (existProduct == null)
                {
                    _productReponsitory.Create(product);
                }
                else
                {
                    existProduct.Image.ImageUrl = product.Image.ImageUrl;
                    existProduct.Name = product.Name;
                    existProduct.Description = product.Description;
                    existProduct.Price = product.Price;
                }
            }

            if (await _restaurantRepository.SaveChangesAsync() < 0)
            {
                throw new MediatorException(ExceptionType.Error, "Error update this restaurant");
            }


        }

    }
}
