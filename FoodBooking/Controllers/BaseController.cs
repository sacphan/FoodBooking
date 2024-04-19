using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : Controller
    {
        protected readonly IMediator _mediator;


        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
