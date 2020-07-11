using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Models
{
    public class Address
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        
        public int HouseNumber { get; set; }

        public string BuildingName { get; set; }

        public string StreetName { get; set; }

        public string Surburb { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }
    }
}
