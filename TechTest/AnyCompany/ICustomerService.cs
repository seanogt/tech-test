
namespace AnyCompany
{
    public interface ICustomerService
    {
        Customer Load(int customerId);

        int Save(Customer customer);

    }
}
