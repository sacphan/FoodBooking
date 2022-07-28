using AutoMapper;

namespace FoodBooking.Mapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Features.Restaurants.MappingProfile());
            });
        }
    }
}
