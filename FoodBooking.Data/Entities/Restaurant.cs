using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBooking.Data.Entities
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string  Description { get; set; }
        public string Title { get; set; }
        public Guid? ImageId { get; set; }
    }
}
