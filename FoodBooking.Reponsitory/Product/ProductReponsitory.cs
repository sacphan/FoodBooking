using FoodBooking.Data;
using FoodBooking.Reponsitory.Base;
using FoodBooking.Reponsitory.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBooking.Reponsitory.Product
{
    public class ProductReponsitory : BaseReponsitory<FoodBooking.Data.Entities.Product>, IProductReponsitory
    {
        public ProductReponsitory(FoodBookingContext context) : base(context)
        {

        }
    }
}
