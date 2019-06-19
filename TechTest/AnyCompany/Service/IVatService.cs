namespace AnyCompany
{
    public interface IVatService
    {
        double GetVatAmount(string countryCode);
    }
}