using FoodBooking.Core.Utils;
using FoodBooking.Data.Entities;
using FoodBooking.Data.Models.Exceptions;
using FoodBooking.Reponsitory.Image;
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
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }

    }

    public class UpdateRestaurantRequestHandler : IRequestHandler<UpdateRestaurantRequest, bool>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IImageReponsitory _imageReponsitory;
        private readonly IConfiguration _configuration;


        public UpdateRestaurantRequestHandler(IRestaurantRepository restaurantRepository, IImageReponsitory imageReponsitory, IConfiguration configuration)
        {
            _restaurantRepository = restaurantRepository;
            _imageReponsitory = imageReponsitory;
            _configuration = configuration;
        }
        public async Task<bool> Handle(UpdateRestaurantRequest request, CancellationToken cancellationToken)
        {
            var restaurantExits = await _restaurantRepository.FindByIdAsync(request.Id);
            if (restaurantExits != null)
            {
                if (!restaurantExits.Name.Equals(request.Name))
                {
                    var nameExits = await _restaurantRepository.FindByNameAsync(request.Name);
                    if (nameExits != null)
                    {
                        throw new MediatorException(ExceptionType.Error, "the name update is already exits");
                    }
                }

                restaurantExits.Name = request.Name;
                restaurantExits.Description = request.Description;
                var filePath = Path.Combine(@$"Public\Images\Restaurant", $"{request.Id}{Path.GetExtension(request.Image.FileName)}");
                var resultUploadFile = await FileUtils.CreateFile(request.Image, filePath);
                if (resultUploadFile)
                {
                    
                    if (restaurantExits.ImageId==null)
                    {
                        var newImage = new Image()
                        {
                            ImageUrl = $@"{_configuration["ApplicationUrl"]}\{filePath}"
                        };
                        _imageReponsitory.Create(newImage);
                        restaurantExits.ImageId = newImage.Id;
                    }
                    else
                    {
                        var exitsImage =await _imageReponsitory.FindByIdAsync(restaurantExits.ImageId.Value);
                        exitsImage.ImageUrl = @$"{_configuration["ApplicationUrl"]}\{filePath}";
                        _imageReponsitory.Update(exitsImage);
                    }    

                }    
                _restaurantRepository.Update(restaurantExits);
                if (await _restaurantRepository.SaveChangesAsync() >= 0)
                {
                    return true;
                }
                throw new MediatorException(ExceptionType.Error, "Error update this restaurant");
            }
            throw new MediatorException(ExceptionType.Error, "This restaurant didn't find");
        }
    }
}
