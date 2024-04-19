using AutoMapper;
using FoodBooking.Features.Restaurants.Dto;
using FoodBooking.Reponsitory.Restaurants;
using MediatR;

namespace FoodBooking.Features.Restaurants.Queries
{
    public class GetRestaurantByIdRequest : IRequest<GetRestaurantByIdReponse>
    {
        public Guid Id { get; set; }
    }


    public class GetRestaurantByIdReponse
    {
        public RestaurantDto? Restaurant { get; set; }
    }

    public class GetRestaurantByIdRequestHandler : IRequestHandler<GetRestaurantByIdRequest, GetRestaurantByIdReponse>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        public GetRestaurantByIdRequestHandler(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }
        public async Task<GetRestaurantByIdReponse> Handle(GetRestaurantByIdRequest request, CancellationToken cancellationToken)
        {
            var restaurant = _mapper.Map<RestaurantDto>(await _restaurantRepository.FindByIdAsync(request.Id));
            return new GetRestaurantByIdReponse() {Restaurant = restaurant };
        }
    }
}
