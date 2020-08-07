namespace AnyCompany.DAL.Repositories
{
    public class OrderRepositoryWrapper : IOrderRepository
    {
        public bool Save(Order order)
        {
            //Use a mapper like AutoMapper here to change Model to user friendly DTO if necessary
            return new OrderRepository().Save(order);
        }
    }
}
