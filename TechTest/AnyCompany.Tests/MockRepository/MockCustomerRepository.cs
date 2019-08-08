// <copyright file="MockCustomerRepository.cs" company="Investec Bank">
// Copyright © Investec Bank 2018
// </copyright>

namespace AnyCompany.Tests.MockRepository
{
    using AnyCompany.Repository;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides a mock implementation of ICustomerRepository for use in unit tests, that starts with two customers.
    /// </summary>
    internal class MockCustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> customers = new List<Customer>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MockCustomerRepository"/> class.
        /// </summary>
        public MockCustomerRepository()
        {
            this.customers.Add(new Customer
            {
                CustomerId = 1,
                Country = "UK",
                DateOfBirth = new DateTime(1990, 01, 01),
                Name = "John Doe",
            });

            this.customers.Add(new Customer
            {
                CustomerId = 2,
                Country = "USA",
                DateOfBirth = new DateTime(1980, 01, 01),
                Name = "Jane Doe",
            });
        }

        /// <inheritdoc/>
        public Customer Load(int customerId)
        {
            if (customerId > 0 && customerId <= 2)
            {
                return this.customers[customerId - 1];
            }
            else
            {
                throw new ApplicationException($"Customer not found with id: {customerId}");
            }
        }

        /// <inheritdoc/>
        public IEnumerable<Customer> LoadAll()
        {
            return this.customers;
        }
    }
}
