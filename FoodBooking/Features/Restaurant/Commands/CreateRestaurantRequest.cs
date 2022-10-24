using AutoMapper;
using FoodBooking.Core.Utils;
using FoodBooking.Data.Entities;
using FoodBooking.Data.Models.Exceptions;
using FoodBooking.Reponsitory.Image;
using FoodBooking.Reponsitory.Restaurants;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FoodBooking.Features.Restaurants.Commands
{
    public class CreateRestaurantsRequest : IRequest<bool>
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }

    }


    public class CreateRestaurantRequestHandler : IRequestHandler<CreateRestaurantsRequest, bool>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        private readonly IImageReponsitory _imageReponsitory;
        private readonly IConfiguration _configuration;


        public CreateRestaurantRequestHandler(IRestaurantRepository restaurantRepository, 
            IImageReponsitory imageReponsitory,
            IConfiguration configuration,
            IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
            _imageReponsitory = imageReponsitory;
            _configuration = configuration;
        }
        public async Task<bool> Handle(CreateRestaurantsRequest request, CancellationToken cancellationToken)
        {
            var restaurantExits = await _restaurantRepository.FindByNameAsync(request.Name);
            if (restaurantExits == null)
            {
                var requestCreate = _mapper.Map<Restaurant>(request);
                _restaurantRepository.Create(requestCreate);
                if (request.Image != null)
                {
                    var newImage = new Image()
                    {
                    };
                    _imageReponsitory.Create(newImage);
                    newImage.ImageUrl = $@"{_configuration["ApplicationUrl"]}\{Path.Combine(@$"Public\Images\Restaurant", $"{newImage.Id}{Path.GetExtension(request.Image.FileName)}")}";
                    requestCreate.ImageId = newImage.Id;
                    var resultUploadFile = await FileUtils.CreateFile(request.Image, Path.Combine(@$"Public\Images\Restaurant", $"{newImage.Id}{Path.GetExtension(request.Image.FileName)}"));
                    if (!resultUploadFile)
                    {
                        throw new MediatorException(ExceptionType.Error, "Error create image");
                    }
                }
                if (await _restaurantRepository.SaveChangesAsync() > 0)
                {
                    return true;
                }
                throw new MediatorException(ExceptionType.Error, "Error create this restaurant");
            }
            throw new MediatorException(ExceptionType.Error, "This restaurant is exits already");
        }
    }
}
