using AutoMapper;
using FoodBooking.Reponsitory.Restaurants;
using MediatR;

namespace FoodBooking.Features.Restaurants.Queries
{
    public class GetRestaurantsRequest : IRequest<GetRestaurantsReponse>
    {
        public string? KeyWord { get; set; } = string.Empty;
        public int Page { get; set; } = 1;
        public int Record { get; set; } = 5;
    }

    public class RestaurantDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }
    }
    public class GetRestaurantsReponse
    {
        public List<RestaurantDto>? Restaurants { get; set; }
        public int Page { get; set; }
        public int TotalRow { get; set; }
        public int TotalPage { get; set; }
    }

    public class GetRestaurantsRequestHandler : IRequestHandler<GetRestaurantsRequest, GetRestaurantsReponse>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        public GetRestaurantsRequestHandler(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }
        public async Task<GetRestaurantsReponse> Handle(GetRestaurantsRequest request, CancellationToken cancellationToken)
        {
            var restaurants = _mapper.Map<List<RestaurantDto>>(await _restaurantRepository.Search(request.KeyWord, request.Page, request.Record));
            var totalRow = 0;
            var totalPage = 0;
            if (restaurants.Any())
            {
                totalRow = await _restaurantRepository.Count(request.KeyWord, request.Page, request.Record);
                totalPage = (int)Math.Ceiling((float)totalRow / request.Record);
            }
            return new GetRestaurantsReponse() { Page = request.Page, Restaurants = restaurants, TotalPage = totalPage, TotalRow = totalRow };
        }
    }

}
