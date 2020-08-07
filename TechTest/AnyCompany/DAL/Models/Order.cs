using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnyCompany
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        //[ForeignKey("Order")]
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public decimal VAT { get; set; }
    }  
}
