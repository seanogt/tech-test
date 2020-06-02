namespace AnyCompany.Models
{
    public class OrderModel
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public double Amount { get; set; }
        internal double VAT { get; set; } //since we set the VAT we do not want to expose it to consuming owners
    }
}
