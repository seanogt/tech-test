using System.ComponentModel.DataAnnotations;

namespace AnyCompany
{
    public class Order
    {
        public int OrderId { get; set; }
        [Required,Range(0.1,double.MaxValue, ErrorMessage = "The value for {0} must be between {1} and {2}.")]
        public double Amount { get; set; }
        [Required]
        public double VAT { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
