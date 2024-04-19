<<<<<<< HEAD
﻿using FoodBooking.Data.Models.Exceptions;
using FoodBooking.Reponsitory.Restaurants;
using MediatR;
using System.ComponentModel.DataAnnotations;
=======
﻿using FoodBooking.Core.Utils;
using FoodBooking.Data;
using FoodBooking.Data.Entities;
using FoodBooking.Data.Models.Exceptions;
using FoodBooking.Reponsitory.Image;
using FoodBooking.Reponsitory.Restaurants;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
>>>>>>> ab94a4c0e989d082633e9a8cfbcdffc03b866fd3

namespace FoodBooking.Features.Restaurants.Commands
{
    public class UpdateRestaurantRequest : IRequest<bool>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
<<<<<<< HEAD
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Title { get; set; }
=======
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }

>>>>>>> ab94a4c0e989d082633e9a8cfbcdffc03b866fd3
    }

    public class UpdateRestaurantRequestHandler : IRequestHandler<UpdateRestaurantRequest, bool>
    {
        private readonly IRestaurantRepository _restaurantRepository;
<<<<<<< HEAD
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
                    restaurantExits.Title = request.Title;
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
                    throw new MediatorException(ExceptionType.Error, "the name update is already exits");
                }
=======
        private readonly IImageReponsitory _imageReponsitory;
        private readonly IConfiguration _configuration;
        private readonly FoodBookingContext _context;



        public UpdateRestaurantRequestHandler(IRestaurantRepository restaurantRepository, IImageReponsitory imageReponsitory, IConfiguration configuration, FoodBookingContext context)
        {
            _restaurantRepository = restaurantRepository;
            _imageReponsitory = imageReponsitory;
            _configuration = configuration;
            _context = context;
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

                if (request.Image!=null)
                {
                    if (restaurantExits.ImageId != null)
                    {
                        FileUtils.DeleteFile(Path.Combine(@$"Public\Images\Restaurant", $"{restaurantExits.ImageId}{Path.GetExtension(request.Image.FileName)}"));
                        var exitsImage = await _imageReponsitory.FindByIdAsync(restaurantExits.ImageId.Value);
                        if (exitsImage != null)
                        {
                            _imageReponsitory.Delete(exitsImage);
                        }
                    }

                    var newImage = new Image()
                    {
                    };
                    _imageReponsitory.Create(newImage);
                    newImage.ImageUrl = $@"{_configuration["ApplicationUrl"]}\{Path.Combine(@$"Public\Images\Restaurant", $"{newImage.Id}{Path.GetExtension(request.Image.FileName)}")}";
                    restaurantExits.ImageId = newImage.Id;
                    var resultUploadFile = await FileUtils.CreateFile(request.Image, Path.Combine(@$"Public\Images\Restaurant", $"{newImage.Id}{Path.GetExtension(request.Image.FileName)}"));
                    if (!resultUploadFile)
                    {
                        throw new MediatorException(ExceptionType.Error, "Error create image");
                    }
                }
               
                _restaurantRepository.Update(restaurantExits);
                if (await _restaurantRepository.SaveChangesAsync() >= 0)
                {
                    return true;
                }
                throw new MediatorException(ExceptionType.Error, "Error update this restaurant");
>>>>>>> ab94a4c0e989d082633e9a8cfbcdffc03b866fd3

            }
            throw new MediatorException(ExceptionType.Error, "This restaurant didn't find");
        }
<<<<<<< HEAD
    }
}
=======

    }
}

>>>>>>> ab94a4c0e989d082633e9a8cfbcdffc03b866fd3
