using System.Collections.Generic;
using System.Linq;
using AnyCompany.Data;
using AnyCompany.Model;
using AnyCompany.Service.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AnyCompany.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(IRepository<Customer> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public List<CustomerDto> LoadAllCustomersWithOrders()
        {
            return _customerRepository.LoadAll().Include(x=>x.Orders).ProjectTo<CustomerDto>(_mapper.ConfigurationProvider).ToList();
        }
    }
}
