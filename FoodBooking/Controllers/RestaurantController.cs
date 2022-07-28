namespace FoodBooking.Controllers;

using FoodBooking.Features.Restaurants.Queries;
using MediatR;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using FoodBooking.Features.Restaurants.Commands;

[ApiController]
[Route("[controller]")]
public class RestaurantController : ControllerBase
{
    private readonly IMediator _mediator;

    public RestaurantController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name ="Search")]
    [ProducesResponseType(typeof(GetRestaurantsReponse),(int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async  Task<IActionResult> Search([FromQuery] GetRestaurantsRequest getRestaurantsRequest)
    {
        try
        {
            var result = await _mediator.Send(getRestaurantsRequest);
            return Ok(result);
        }
        catch 
        {

            return NotFound();
        }
    }

    [HttpPost(Name = "Create")]
    [ProducesResponseType(typeof(GetRestaurantsReponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create(CreateRestaurantsRequest createRestaurantsRequest)
    {

            var result = await _mediator.Send(createRestaurantsRequest);
            if (result)
            {
                return StatusCode(201);
            }
            else
            {
                return BadRequest();
            }      
       
    }
}

