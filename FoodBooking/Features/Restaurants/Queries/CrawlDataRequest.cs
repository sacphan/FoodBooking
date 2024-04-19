using FoodBooking.Features.Restaurants.Queries;
using HtmlAgilityPack;
using MediatR;
using PuppeteerSharp;

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
        public CrawlDataRequestRequestHandler()
        {
                
        }
        public async Task<CrawlDataReponse> Handle(CrawlDataRequest request, CancellationToken cancellationToken)
        {
            var result = new CrawlDataReponse();
            var html = await GetHtmlContentAsync(request.sourceLink);
            var test = ParseHtml(html);
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

        private List<string> ParseHtml(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var nodes = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'item-restaurant-row')]");
            foreach (var node in nodes)
            {
                // Extract image URL from the img element
                var imageNode = node.SelectSingleNode(".//div[contains(@class, 'item-restaurant-img')]//img");
                string imageUrl = imageNode != null ? imageNode.GetAttributeValue("src", string.Empty) : string.Empty;

                // Extract item name
                var nameNode = node.SelectSingleNode(".//div[contains(@class, 'item-restaurant-info')]//h2[contains(@class, 'item-restaurant-name')]");
                string itemName = nameNode != null ? nameNode.InnerText.Trim() : string.Empty;

                // Extract item description
                var descNode = node.SelectSingleNode(".//div[contains(@class, 'item-restaurant-info')]//div[contains(@class, 'item-restaurant-desc')]");
                string itemDescription = descNode != null ? descNode.InnerText.Trim() : string.Empty;
                Console.WriteLine(node.OuterHtml);
            }
            List<string> wikiLink = new List<string>();


            return wikiLink;
        }

    }
}
