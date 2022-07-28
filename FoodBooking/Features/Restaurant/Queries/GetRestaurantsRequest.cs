using AutoMapper;
using FoodBooking.Reponsitory.Restaurants;
using MediatR;

namespace FoodBooking.Features.Restaurants.Queries
{
    public class GetRestaurantsRequest : IRequest<List<GetRestaurantsReponse>>
    {
        public string KeyWord { get; set; } = string.Empty;
        public int Page { get; set; } = 1;
        public int Record { get; set; } = 10;
    }
    public class GetRestaurantsReponse
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }
        public Guid? ImageId { get; set; }
    }

    public class GetRestaurantsRequestHandler : IRequestHandler<GetRestaurantsRequest, List<GetRestaurantsReponse>>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        public GetRestaurantsRequestHandler(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }
        public async Task<List<GetRestaurantsReponse>> Handle(GetRestaurantsRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<GetRestaurantsReponse>>(await _restaurantRepository.Search(request.KeyWord, request.Page, request.Record));
        }
    }

}
