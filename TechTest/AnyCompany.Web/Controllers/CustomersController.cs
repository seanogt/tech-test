using System.Collections.Generic;
using System.Threading.Tasks;
using AnyCompany.Service.Container;
using AnyCompany.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnyCompany.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private IContainer _container;

        public CustomersController(IContainer container)
        {
            this._container = container;
        }
        
        [HttpGet("{customerId}/orders")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Order>>> GetCustomerOrders(string customerId)
        {
            return new ActionResult<IEnumerable<Order>>(await this._container.OrdersFacade.GetOrdersByCustomer(customerId));
        }
    }
}