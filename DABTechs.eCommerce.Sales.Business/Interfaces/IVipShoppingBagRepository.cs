using DABTechs.eCommerce.Sales.Business.Models.ShoppingBag;
using System.Threading.Tasks;

namespace DABTechs.eCommerce.Sales.Business.Interfaces
{
    public interface IVipShoppingBagRepository
    {
        Task<Bag> GetShoppingBagAsync(string id);

        Task<Bag> CreateShoppingBagAsync(Bag bag);
    }
}
