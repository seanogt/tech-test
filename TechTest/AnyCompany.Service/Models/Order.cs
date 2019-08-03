namespace AnyCompany.Service.Models
{
    /// <summary>
    /// Describes an order
    /// Can be described in an interface as well, but it doesn't add any value.
    /// </summary>
    public class Order
    {
        public Order(string orderId, double amount, double vat, string customerId)
        {
            this.OrderId = orderId;
            this.Amount = amount;
            this.VAT = vat;
            this.CustomerId = customerId;
        }
        /// <summary>
        /// I wouldn't' use an integer as an identifier as this is not scalable enough, but
        /// I am assuming that the deed is already done, so leaving it as an integer.
        /// </summary>
        public string OrderId { get; private set; }
        
        /// <summary>
        /// The CustomerId is the only "not-related" field of an order.
        /// But considering the fact that all orders have an owner in almost every ecomm system in the world,
        /// and in order to avoid too many levels of indirection and abstraction, I am allowing myself to add the CustomerId
        /// as a field on the Order Model.
        /// </summary>
        public string CustomerId { get; private set; }
        public double Amount { get; private set; }
        public double VAT { get; private set; }
    }
}
