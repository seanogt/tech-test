using System;
using System.Collections.Generic;
using System.Text;
using AnyCompany.Service.DTOs;

namespace AnyCompany.Service
{
    public interface ICustomerService
    {
        List<CustomerDto> LoadAllCustomersWithOrders();
    }
}
