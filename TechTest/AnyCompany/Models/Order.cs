namespace AnyCompany.Models
{
    /// <summary>
    /// This is the Order Model
    /// </summary>
    public class Order
    {
        public int OrderId { get; set; }
        public double Amount { get; set; }
        public double VAT { get; set; }

        public int CustomerId { get; set; }
    }
}
