using FoodBooking.Data.Models.Exceptions;
using FoodBooking.Reponsitory.Restaurants;
using MediatR;

namespace FoodBooking.Features.Restaurants.Commands
{
    public class DeleteRestaurantRequest : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteRestaurantRequestHandler : IRequestHandler<DeleteRestaurantRequest, bool>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        public DeleteRestaurantRequestHandler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }
        public async Task<bool> Handle(DeleteRestaurantRequest request, CancellationToken cancellationToken)
        {
            var restaurantExits = await _restaurantRepository.FindByIdAsync(request.Id);
            if (restaurantExits != null)
            {

                _restaurantRepository.Delete(restaurantExits);
                if (await _restaurantRepository.SaveChangesAsync() > 0)
                {
                    return true;
                }
                throw new MediatorException(ExceptionType.Error, "Error delete this restaurant");


            }
            throw new MediatorException(ExceptionType.Error, "This restaurant didn't find");

        }
    }
}
