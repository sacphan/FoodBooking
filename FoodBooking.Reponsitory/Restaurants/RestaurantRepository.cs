using FoodBooking.Data;
using FoodBooking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBooking.Reponsitory.Restaurants
{
    public class RestaurantRepository : BaseReponsitory, IRestaurantRepository
    {
        public RestaurantRepository(FoodBookingContext context) : base(context)
        {

        }
        public async Task<List<Restaurant>> Search(string keyword, int page, int record)
        {
            return await _context.Restaurants.AsNoTracking()
                .Where(r => r.Name.ToLower()
                .Contains(keyword) || keyword == null)
                .Skip((page - 1) * record)
                .Take(record)
                .ToListAsync();
        }

        public async Task<bool> CreateAsync(Restaurant newRestaurant)
        {
            await _context.AddAsync(newRestaurant);
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<Restaurant?> FindByNameAsync(string name)
        {
            return await _context.Restaurants.FirstOrDefaultAsync(r => r.Name == name);
        }
    }
}
