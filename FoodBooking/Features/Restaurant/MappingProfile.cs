﻿using AutoMapper;
using FoodBooking.Data.Entities;
using FoodBooking.Features.Restaurants.Commands;
using FoodBooking.Features.Restaurants.Queries;

namespace FoodBooking.Features.Restaurants
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>();

            CreateMap<CreateRestaurantsRequest, Restaurant>();

        }
    }
}
