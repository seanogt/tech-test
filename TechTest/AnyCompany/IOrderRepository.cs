using System.Data;

namespace AnyCompany.Utilities
{
   public interface IOrderRepository
    {
        void Save(Order order, int customerId);

        DataSet GellAllOrders();
    }
}
