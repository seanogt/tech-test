using AnyCompany.Services.Constants;

namespace AnyCompany.Services.Helpers
{
    internal static class VatCalculator
    {
        internal static double GetVat(string country)
        {
            return country == "UK" ? VatConstants.UkVat : VatConstants.RowVat;
        }
    }
}
