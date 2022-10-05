using FoodBooking.Data;
using FoodBooking.Data.Entities;
using FoodBooking.Reponsitory.Base;
using FoodBooking.Reponsitory.Restaurants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBooking.Reponsitory.Image
{
    public class ImageReponsitory : BaseReponsitory<FoodBooking.Data.Entities.Image>, IImageReponsitory
    {
        public ImageReponsitory(FoodBookingContext context) : base(context)
        {

        }


    }
}
