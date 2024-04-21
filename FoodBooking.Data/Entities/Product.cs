using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

﻿namespace FoodBooking.Data.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public Guid RestaurantId { get; set; }
        public Guid? ImageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Inventory { get; set; }
        public DateTime CreateDate { get; set; }
        public Restaurant Restaurant { get; set; }
        public Image Image { get; set; }
    }
}
