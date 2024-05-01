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

        public async Task<Restaurant?> FindByLinkCrawlAsync(string linkCrawl)
        {
            return await _context.Restaurants.Include(r=>r.Image).FirstOrDefaultAsync(r => r.LinkCrawl == linkCrawl);
        }

        public async Task<Restaurant?> FindDetailByIdAsync(Guid id)
        {
            return await _context.Restaurants
                .Include(r => r.Image)
                .Include(r => r.Products).ThenInclude(p=>p.Image)
                .FirstOrDefaultAsync(r=>r.Id == id);
        }

        public async Task<List<Restaurant>> Search(string keyword, int page, int record)
        {
            var keySeach = keyword.ToLower();
            return await _context.Restaurants.AsNoTracking()
                .Where(r => r.Name.ToLower().Contains(keySeach)
                || r.Description.ToLower().Contains(keySeach)
                || keyword == null)
                .OrderByDescending(x => x.CreateDate)
                .Skip((page - 1) * record)
                .Take(record)
                .Include(x=>x.Image)
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
