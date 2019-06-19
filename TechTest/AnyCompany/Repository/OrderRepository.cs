using AnyCompany.Entity;

namespace AnyCompany.Repository
{
    public class OrderRepository
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderRepository(IUnitOfWork UnitOfwork)
        {
            unitOfWork = UnitOfwork;
        }

        public void Save(Order order)
        {
            unitOfWork.Orders.Add(order);

            unitOfWork.SaveChangesAsync();
        }
    }
}
