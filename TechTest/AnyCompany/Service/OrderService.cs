using AnyCompany.Entity;
using AnyCompany.Repository;
using System.Linq;

namespace AnyCompany.Service
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository;
        private readonly IUnitOfWork unitOfWork;

        public OrderService(IUnitOfWork UnitOfwork)
        {
            unitOfWork = UnitOfwork;
            orderRepository = new OrderRepository(UnitOfwork);
        }

        public bool PlaceOrder(Order order)
        {
            bool orderPlaced = false;

            if (IsValidOrder(order))
            {
                try
                {
                    Customer customer = CustomerRepository.Load(order.CustomerId, unitOfWork);

                    var applicableVat = unitOfWork.VATs.SingleOrDefault<VAT>(s => string.Compare(s.Country, customer.Country, true) == 0);

                    if (applicableVat != null)
                        order.VAT = applicableVat.ApplyVat;
                    else
                        order.VAT = 0;

                    orderRepository.Save(order);

                    orderPlaced = true;
                }
                catch
                {
                    //Logging and exception handling
                }
            }

            return orderPlaced;
        }

        public bool IsValidOrder(Order order)
        {
            return order.Amount > 0;
        }
    }
}
