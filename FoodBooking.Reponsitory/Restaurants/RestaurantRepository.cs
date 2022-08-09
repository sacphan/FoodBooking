using FoodBooking.Data;
using FoodBooking.Data.Entities;
using FoodBooking.Reponsitory.Base;
using Microsoft.EntityFrameworkCore;

namespace FoodBooking.Reponsitory.Restaurants
{
    public class RestaurantRepository : BaseReponsitory<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(FoodBookingContext context) : base(context)
        {

        }

        public async Task<Restaurant?> FindByNameAsync(string name)
        {
            return await _context.Restaurants.FirstOrDefaultAsync(r => r.Name == name);
        }

        public async Task<List<Restaurant>> Search(string keyword, int page, int record)
        {
            return await _context.Restaurants.AsNoTracking()
                .Where(r => r.Name.ToLower()
                .Contains(keyword) || keyword == null)
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * record)
                .Take(record)
                .ToListAsync();
        }

        public async Task<int> Count(string keyword, int page, int record)
        {
            return await _context.Restaurants.AsNoTracking()
                .Where(r => r.Name.ToLower()
                .Contains(keyword) || keyword == null)
                .CountAsync();
        }
    }
}
