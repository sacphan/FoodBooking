using FoodBooking.Data;
using FoodBooking.Reponsitory.Base;
using FoodBooking.Data.Entities;
using FoodBooking.Reponsitory.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FoodBooking.Reponsitory.Products
{
    public class ProductReponsitory : BaseReponsitory<Product>, IProductReponsitory
    {
        public ProductReponsitory(FoodBookingContext context) : base(context)
        {

        }

        public async Task<Product?> FindByNameAndRestaurantId(string name, Guid restaurantId)
        {
            return await _context.Products.Include(p=>p.Image).FirstOrDefaultAsync(p => p.RestaurantId == restaurantId && p.Name.Equals(name));
        }
    }
}
