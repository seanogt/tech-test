using AnyCompany.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AnyCompany
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _orderDbContext;
        public OrderRepository(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }
        public void Save(Order order)
        {
            _orderDbContext.Orders.Add(order);
            _orderDbContext.SaveChanges();
        }

        public List<Order> LoadAll()
        {
            return _orderDbContext.Orders.ToList();
        }
    }
}
