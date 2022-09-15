using FoodBooking.Data.Models.Exceptions;
using FoodBooking.Reponsitory.Restaurants;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FoodBooking.Features.Restaurants.Commands
{
    public class UpdateRestaurantRequest : IRequest<bool>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
    }

    public class UpdateRestaurantRequestHandler : IRequestHandler<UpdateRestaurantRequest, bool>
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
                var nameExits = await _restaurantRepository.FindByNameAsync(request.Name);
                if (nameExits == null)
                {
                    restaurantExits.Name = request.Name;
                    restaurantExits.Description = request.Description;
                    _restaurantRepository.Update(restaurantExits);
                    if (await _restaurantRepository.SaveChangesAsync() >= 0)
                    {
                        return true;
                    }
                    throw new MediatorException(ExceptionType.Error, "Error update this restaurant");
                }
                else
                {
                    if (nameExits.Id!=request.Id)
                    {
                        throw new MediatorException(ExceptionType.Error, "the name update is already exits");
                    }
                    return true;
                }

            }
            throw new MediatorException(ExceptionType.Error, "This restaurant didn't find");
        }
    }
}
