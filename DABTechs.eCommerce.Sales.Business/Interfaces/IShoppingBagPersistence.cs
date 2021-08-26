using DABTechs.eCommerce.Sales.Business.Models.ShoppingBag;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DABTechs.eCommerce.Sales.Business.Interfaces
{
    public interface IShoppingBagPersistence
    {
        Task<Bag> CreateShoppingBagAsync(Bag bag);

        Task<Bag> GetShoppingBagAsync(string id);
    }
}
