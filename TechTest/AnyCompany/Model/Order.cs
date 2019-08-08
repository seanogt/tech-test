// <copyright file="Order.cs" company="Investec Bank">
// Copyright © Investec Bank 2018
// </copyright>

namespace AnyCompany
{
    /// <summary>
    /// The order database model.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or sets a unique identifier for this order.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the amount (in GBP?) this order is for.
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Gets or sets the rate of VAT (Value Added Tax) due on this order.
        /// </summary>
        public double VAT { get; set; }

        /// <summary>
        /// Gets or sets the id of the customer who placed this order.
        /// </summary>
        public int CustomerId { get; set; }
    }
}
