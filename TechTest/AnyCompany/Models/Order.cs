namespace AnyCompany.Models
{
    public class Order : Base
    {
        public int OrderId { get; set; }
        public double Amount { get; set; }
        public double VAT { get; set; }
    }
}
