using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AuthSystem.WebApp.Application.User;

namespace AuthSystem.WebApp.Controllers
{
    [Area("api")]
    [ApiController]    
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[area]/[controller]/[action]")]
        public async Task<IActionResult> Create(Create.Command command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}