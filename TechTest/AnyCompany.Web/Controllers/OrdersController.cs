using System.Threading.Tasks;
using AnyCompany.Service.Container;
using AnyCompany.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public async Task<ActionResult<Order>> GetOrderById()
        {
            return new ActionResult<Order>(await this._container.OrdersFacade.GetOrderById("orderId"));
        }

        [HttpPost]
        [Route("/")]
        public async Task<ActionResult<string>> CreateOrder(
            // Ideally, add a JSON representer of an order without the generated fields (i.e OrderId)
            // For now, just using a JSON string
            string orderData
        )
        {
            // This wont work, as we need a serialisable Order representer with data that can be set, and auto-generated ID.
            // But for the task's sake, keeping it this way.
            var orderToCreate = JsonConvert.DeserializeObject<Order>(orderData);

            var createdId =  await _container.OrdersFacade.CreateOrderForCustomer(orderToCreate);
            
            return new ActionResult<string>(createdId);
        }
    }
}