using DABTechs.eCommerce.Sales.Business.Interfaces;
using DABTechs.eCommerce.Sales.Business.Models.ShoppingBag;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DABTechs.eCommerce.Sales.Business.Repositories
{
    public class VipShoppingBagRepository : IVipShoppingBagRepository
    {
        private readonly IShoppingBagPersistence _persistence;

        public VipShoppingBagRepository(IShoppingBagPersistence persistence)
        {
            _persistence = persistence;
        }

        public Task<Bag> CreateShoppingBagAsync(Bag bag)
        {
            return _persistence.CreateShoppingBagAsync(bag);
        }

        public Task<Bag> GetShoppingBagAsync(string id)
        {
            return _persistence.GetShoppingBagAsync(id);
        }
    }
}
