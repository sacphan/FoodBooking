namespace FoodBooking.Controllers;

using FoodBooking.Features.Restaurants.Commands;
using FoodBooking.Features.Restaurants.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

[ApiController]
[Route("[controller]")]
public class RestaurantController : ControllerBase
{
    private readonly IMediator _mediator;

    public RestaurantController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "Search")]
    [ProducesResponseType(typeof(GetRestaurantsReponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Search([FromQuery] GetRestaurantsRequest getRestaurantsRequest)
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
        await _mediator.Send(createRestaurantsRequest);
        return StatusCode(201);
    }

    [HttpPut(Name = "Update")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]

    public async Task<IActionResult> Update(UpdateRestaurantRequest createRestaurantsRequest)
    {
        await _mediator.Send(createRestaurantsRequest);
        return Ok();
    }

    [HttpDelete(Name = "Delete")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]

    public async Task<IActionResult> Delete(DeleteRestaurantRequest deleteRestaurantRequest)
    {
        await _mediator.Send(deleteRestaurantRequest);
        return Ok();
    }
}

