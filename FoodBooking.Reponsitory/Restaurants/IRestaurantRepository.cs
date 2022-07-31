using FoodBooking.Data.Entities;

namespace FoodBooking.Reponsitory.Restaurants
{
    public interface IRestaurantRepository : IBaseReponsitory<Restaurant>
    {
        Task<List<Restaurant>> Search(string keyword, int page, int record);
        Task<Restaurant?> FindByNameAsync(string name);
    }
}
