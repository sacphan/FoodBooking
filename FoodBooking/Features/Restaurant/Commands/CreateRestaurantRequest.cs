using AutoMapper;
using FoodBooking.Data.Entities;
using FoodBooking.Reponsitory.Restaurants;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FoodBooking.Features.Restaurants.Commands
{
    public class CreateRestaurantsRequest : IRequest<bool>
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Title { get; set; }
        public Guid? ImageId { get; set; }
    }   
        

    public class CreateRestaurantRequestHandler : IRequestHandler<CreateRestaurantsRequest, bool>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public CreateRestaurantRequestHandler(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreateRestaurantsRequest request, CancellationToken cancellationToken)
        {
            var restaurantExits =await _restaurantRepository.FindByNameAsync(request.Name);
            if (restaurantExits == null)
            {
                return await _restaurantRepository.CreateAsync(_mapper.Map<Restaurant>(request));
            }
            throw new BadHttpRequestException("This restaurant is exits already");
        }
    }
}
