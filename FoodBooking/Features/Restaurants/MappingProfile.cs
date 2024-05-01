using AutoMapper;
using FoodBooking.Data.Entities;
using FoodBooking.Features.Restaurants.Commands;
using FoodBooking.Features.Restaurants.Dto;
using FoodBooking.Features.Restaurants.Queries;
using System.Globalization;

namespace FoodBooking.Features.Restaurants
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap< Restaurant, RestaurantDto>();

            CreateMap<Image, ImageDto>();

            CreateMap<CreateRestaurantsRequest, Restaurant>()
                .ForMember(x => x.Image, opt => opt.Ignore());

            CreateMap<Product, ProductDto>()
                .ForMember(p=> p.Price, opt =>
                {

                    opt.MapFrom(p => p.Price.ToString("N0"));
                });

        }
    }
}
