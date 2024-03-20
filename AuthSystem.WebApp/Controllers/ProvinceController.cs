using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AuthSystem.WebApp.Application.Province;

namespace AuthSystem.WebApp.Application.Features.Country
{
    [ApiController]
    [Area("api")]
    public class ProvinceController : Controller
    {
        private readonly IMediator _mediator;

        public ProvinceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[area]/[controller]/[action]")]
        public async Task<IActionResult> GetByCountryId(GetProvincesByCountryId.Query req)
        {
            return Ok(await _mediator.Send(req));
        }
    }
}