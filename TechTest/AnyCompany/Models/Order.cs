namespace AnyCompany
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }     
        public decimal Amount { get; set; } //changed double to decimal. When handling money, decimal type gives a high level of accuracy / easy to avoid rounding errors. 
        public decimal VAT { get; set; }
    }
}
