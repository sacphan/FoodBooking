<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBooking.Data.Entities
=======
﻿namespace FoodBooking.Data.Entities
>>>>>>> ab94a4c0e989d082633e9a8cfbcdffc03b866fd3
{
    public class Product
    {
        public Guid Id { get; set; }
<<<<<<< HEAD
        public Guid RestaurantId { get; set; }
        public Guid ImageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Inventory { get; set; }
        public DateTime CreateDate { get; set; }
        public Restaurant Restaurant { get; set; }
        public Image Image { get; set; }
=======
        public string Name { get; set; }
        public Guid RestaurantId { get; set; }
        public Guid? ImageId { get; set; }
        public double Price { get; set; }
        public Image Image { get; set; }
        public Restaurant Restaurant { get; set; }

>>>>>>> ab94a4c0e989d082633e9a8cfbcdffc03b866fd3
    }
}
