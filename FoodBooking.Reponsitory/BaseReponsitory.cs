using FoodBooking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBooking.Reponsitory
{
    public abstract class BaseReponsitory
    {
        protected readonly FoodBookingContext _context;

        protected BaseReponsitory(FoodBookingContext context)
        {
            _context = context;
        }
    }
}
