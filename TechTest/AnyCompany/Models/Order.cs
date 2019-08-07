namespace AnyCompany
{
    public class Order
    {
        public int OrderId { get; set; }
        public double Amount { get; set; }
        public double VAT { get; set; }
        public int CustomerId { get; set; }

        public bool IsValid()
        {
            if (this.Amount == 0)
            {
                return false;
            }

            return true;
        }

        public Order SetVAT(Customer customer)
        {
             
            if (customer.Country == "UK")
                this.VAT = 0.2d;
            else
                this.VAT = 0;

            return this;
        }
        
    }
}
