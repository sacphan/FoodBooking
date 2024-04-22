using FoodBooking.Data.Entities;
using FoodBooking.Reponsitory.Base;

namespace FoodBooking.Reponsitory.Restaurants
{
    public interface IRestaurantRepository : IBaseReponsitory<Restaurant>
    {
        Task<List<Restaurant>> Search(string keyword, int page, int record);
        Task<Restaurant?> FindByNameAsync(string name);
        Task<Restaurant?> FindByLinkCrawlAsync(string linkCrawl);
        Task<int> Count(string keyword, int page, int record);
    }
}
