namespace AnyCompany.BLL.Helpers
{
    public static class BusinessRulesHelper
    {
        public static decimal DetermineVat(string Country)
        {
            //Country should be made into its on Model ideally as it could contain information about itself; Postal Code, City, Town etc 
            //"UK" should not be hardcoded or a string. There should be a dynamic but 
            //consistent source from which to pull these values from. Like a paid for "Countries" webservice or something.
            return (decimal)(!string.IsNullOrEmpty(Country) && Country.ToUpper() == "UK" ? 0.2 : 0);
        }
        public static bool IsValidOrderAmount(decimal orderAmount)
        {
            return orderAmount > 0 ? true : false;
        }
    }
}
