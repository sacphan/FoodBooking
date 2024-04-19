using AutoMapper;
<<<<<<< HEAD
using FoodBooking.Data.Entities;
using FoodBooking.Data.Models.Exceptions;
=======
using FoodBooking.Core.Utils;
using FoodBooking.Data.Entities;
using FoodBooking.Data.Models.Exceptions;
using FoodBooking.Reponsitory.Image;
>>>>>>> ab94a4c0e989d082633e9a8cfbcdffc03b866fd3
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
<<<<<<< HEAD
        [Required]
        public string? Title { get; set; }
        public Guid? ImageId { get; set; }
=======
        public IFormFile? Image { get; set; }

>>>>>>> ab94a4c0e989d082633e9a8cfbcdffc03b866fd3
    }


    public class CreateRestaurantRequestHandler : IRequestHandler<CreateRestaurantsRequest, bool>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
<<<<<<< HEAD

        public CreateRestaurantRequestHandler(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
=======
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
>>>>>>> ab94a4c0e989d082633e9a8cfbcdffc03b866fd3
        }
        public async Task<bool> Handle(CreateRestaurantsRequest request, CancellationToken cancellationToken)
        {
            var restaurantExits = await _restaurantRepository.FindByNameAsync(request.Name);
            if (restaurantExits == null)
            {
<<<<<<< HEAD
                _restaurantRepository.Create(_mapper.Map<Restaurant>(request));
=======
                var requestCreate = _mapper.Map<FoodBooking.Data.Entities.Restaurant>(request);
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
>>>>>>> ab94a4c0e989d082633e9a8cfbcdffc03b866fd3
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
