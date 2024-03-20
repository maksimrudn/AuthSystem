using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AuthSystem.WebApp.Application.Country;

namespace AuthSystem.WebApp.Controllers
{
    [Area("api")]
    [ApiController]    
    public class CountryController : Controller
    {
        private readonly IMediator _mediator;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[area]/[controller]/[action]")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAll.Query()));
        }
    }
}