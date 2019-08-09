using System.Collections.Generic;
using AnyCompany.Services.Dtos;

namespace AnyCompany.Services.Services
{
    public interface ICustomerOrderService
    {
        IEnumerable<CustomerOrdersDto> GetAllCustomerWithOrders();
    }
}
