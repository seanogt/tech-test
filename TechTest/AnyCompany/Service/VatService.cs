using System;
using System.Collections.Generic;

namespace AnyCompany
{
    public class VatService : IVatService
    {
        public double GetVatAmount(string countryCode)
        {
            double vat = 0;
            if (string.Equals(countryCode, "UK", System.StringComparison.InvariantCultureIgnoreCase))
                vat = 0.2d;

            return vat;
        }
    }
}
