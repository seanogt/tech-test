using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AnyCompany.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _Mediator;

        public CustomerController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpPost]
        public async Task Add(AddCommand command) => await _Mediator.Send(command);
    }
}
