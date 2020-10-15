using System;

using System.Collections.Specialized;
namespace AnyCompany
{
    class Vat
    {

        /// <summary>
        /// Finds the correct VAT rate in settings based on the given country.
        /// </summary>
        /// <param name="countryCode">The country code to use for the VAT lookup.</param>
        public static double GetVatRateByCountry(string countryCode)
        {
            StringCollection vatSettings = Properties.Settings.Default.VatSettings;

            foreach (string setting in vatSettings)
            {
                // Confirm that this settings matches the 'CountryCode:Rate' pattern, split into an array, and confirm the second array element is indeed double compatible.
                if (setting.Contains(":"))
                {
                    string[] settingsPart = setting.Split(':');

                    if (settingsPart.Length == 2 && settingsPart[0].Equals(countryCode, StringComparison.OrdinalIgnoreCase))
                    {
                        double testVal = 0;

                        if (double.TryParse(settingsPart[1], out testVal))
                        {
                            return testVal;
                        }
                    }
                }
            }

            return 0;
        }
    }
}
