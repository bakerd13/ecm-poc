using DABTechs.eCommerce.Sales.Providers.Menu.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DABTechs.eCommerce.Sales.Providers.Menu
{
    public interface IVipMenu
    {
        Task<MegaNav> GetMegaNav();
    }
}
