namespace FoodBooking.Controllers;

using FoodBooking.Features.Restaurants.Commands;
using FoodBooking.Features.Restaurants.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

[ApiController]
public class RestaurantController : BaseController
{
    public RestaurantController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("Search")]
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

    [HttpGet(Name = "Get")]
    [ProducesResponseType(typeof(GetRestaurantByIdReponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Get([FromQuery] GetRestaurantByIdRequest getRestaurantsRequest)
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
    public async Task<IActionResult> Create([FromForm] CreateRestaurantsRequest createRestaurantsRequest)
    {
        await _mediator.Send(createRestaurantsRequest);
        return StatusCode(201);
    }

    [HttpPut(Name = "Update")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]

    public async Task<IActionResult> Update([FromForm] UpdateRestaurantRequest createRestaurantsRequest)
    {
        await _mediator.Send(createRestaurantsRequest);
        return Ok();
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]

    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(await _mediator.Send(new DeleteRestaurantRequest() { Id=id}));
    }

    [HttpPost("CrawlData")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]

    public async Task<IActionResult> CrawlData(CrawlDataRequest crawlDataRequest)
    {
        await _mediator.Send(crawlDataRequest);
        return Ok();
    }
}

