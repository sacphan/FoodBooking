using AutoMapper;
using FoodBooking.Features.Restaurants.Queries;
using FoodBooking.Data.Entities;
using FoodBooking.Features.Restaurants.Commands;

namespace FoodBooking.Features.Restaurants
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Restaurant, GetRestaurantsReponse>();

            CreateMap<CreateRestaurantsRequest, Restaurant>();

        }
    }
}
