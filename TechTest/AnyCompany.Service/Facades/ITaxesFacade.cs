using System.Threading.Tasks;

namespace AnyCompany.Service.Facades
{
    /// <summary>
    /// Returns tax value based on the tax type.
    /// </summary>
    public interface ITaxesFacade 
    {
        Task<decimal> GetTaxByType(string taxType);
    }
}