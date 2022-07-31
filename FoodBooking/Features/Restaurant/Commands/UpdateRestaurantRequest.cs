using FoodBooking.Reponsitory.Restaurants;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FoodBooking.Features.Restaurants.Commands
{
    public class UpdateRestaurantRequest:IRequest<bool>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Title { get; set; }
    }

    public class UpdateRestaurantRequestHandler:IRequestHandler<UpdateRestaurantRequest,bool>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        public UpdateRestaurantRequestHandler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }
        public async Task<bool> Handle(UpdateRestaurantRequest request, CancellationToken cancellationToken)
        {
            var restaurantExits = await _restaurantRepository.FindByIdAsync(request.Id);
            if (restaurantExits != null)
            {
                return await _restaurantRepository.CreateAsync(_mapper.Map<Restaurant>(request));
            }
            throw new BadHttpRequestException("This restaurant didn't find");
        }
    }
}
