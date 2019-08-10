using AnyCompany.Services.Constants;

namespace AnyCompany.Services.Helpers
{
    internal static class VatCalculator
    {
        // small bit of logic so kept in a static helper method - could move behind an interface in the future
        internal static double GetVat(string country)
        {
            return country == "UK" ? VatConstants.UkVat : VatConstants.RowVat;
        }
    }
}
