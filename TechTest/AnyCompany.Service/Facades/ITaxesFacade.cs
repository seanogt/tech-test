using System.Threading.Tasks;

namespace AnyCompany.Service.Facades
{
    /// <summary>
    /// Returns tax value based on the tax type.
    /// </summary>
    public interface ITaxesFacade 
    {
        /// <summary>
        /// Returns the tax by the specified type.
        /// //TODO: Advanced filtering by more parameters, as country, state, etc.
        /// </summary>
        /// <param name="taxType">i.e 'VAT'</param>
        /// <returns></returns>
        Task<decimal> GetTaxByType(string taxType);
    }
}