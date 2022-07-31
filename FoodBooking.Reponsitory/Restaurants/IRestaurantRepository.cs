using FoodBooking.Data.Entities;

namespace FoodBooking.Reponsitory.Restaurants
{
    public interface IRestaurantRepository
    {
        Task<List<Restaurant>> Search(string keyword, int page, int record);
        Task<Restaurant?> FindByNameAsync(string name);
        Task<Restaurant?> FindByIdAsync(Guid id);
        Task<bool> CreateAsync(Restaurant newRestaurant);
    }
}
