using Microsoft.Extensions.FileProviders;
using DABTechs.eCommerce.Sales.Providers.Menu.Mapper;
using DABTechs.eCommerce.Sales.Providers.Menu.Models;
using System.Threading.Tasks;

namespace DABTechs.eCommerce.Sales.Providers.Menu
{
    public class VipMenu : IVipMenu
    {
        private readonly IFileProvider _fileProvider;

        public VipMenu(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public async Task<MegaNav> GetMegaNav()
        {

            var file = _fileProvider.GetFileInfo("Menu.xml");

            if (file.Exists)
            {
                var mapper = new MegaNavMapper();
                return await mapper.Map(file);
            }

            return new MegaNav();
        }
    }
}
