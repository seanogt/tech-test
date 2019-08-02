using System.Threading.Tasks;
using AnyCompany.Service.Container;
using AnyCompany.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnyCompany.Web.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IContainer _container;

        public OrdersController(IContainer container)
        {
            this._container = container;
        }
        
        [HttpGet]
        [Route("/{orderId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Order>> GetCustomerOrders()
        {
            return new ActionResult<Order>(await this._container.OrdersFacade.GetOrderById("orderId"));
        }
    }
}