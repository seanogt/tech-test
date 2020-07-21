
namespace AnyCompany
{
    public class CustomerService : ICustomerService
    {
        public Customer Load(int customerId)
        {
            Customer customer =  CustomerRepository.Load(customerId);

            return customer;
        }

        public int Save(Customer customer)
        {
            return CustomerRepository.Save(customer);
        }
    }
}
