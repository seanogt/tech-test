// <copyright file="Customer.cs" company="Investec Bank">
// Copyright © Investec Bank 2018
// </copyright>

namespace AnyCompany
{
    using System;

    /// <summary>
    /// The customer database model.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets a description of the country in which the customer lives.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets this customer's date of birth.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets this customer's full name.
        /// </summary>
        public string Name { get; set; }
    }
}
