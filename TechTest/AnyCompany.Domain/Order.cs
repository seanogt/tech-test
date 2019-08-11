using System.ComponentModel.DataAnnotations.Schema;

namespace AnyCompany.Domain
{
    public class Order
    {
        public int OrderId { get; set; }
        public double Amount { get; set; }
        public double VAT { get; set; }
        [ForeignKey("CustomersId")]
        [InverseProperty("Orders")]
        public Customer Customer { get; set; }
        public virtual int CustomersId { get; set; }
    }
}
