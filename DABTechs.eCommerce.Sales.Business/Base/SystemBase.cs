using Microsoft.Extensions.Options;
using DABTechs.eCommerce.Sales.Common.Config;

namespace DABTechs.eCommerce.Sales.Business.Base
{
    /// <summary>
    /// The VIP Sale System Base.
    /// </summary>
    public class SystemBase
    {
        #region Services

        protected AppSettings AppSettings { get; set; }

        #endregion Services

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemBase"/> class.
        /// </summary>
        public SystemBase(IOptions<AppSettings> appSettings)
        {
            AppSettings = appSettings.Value;
        }

        #endregion Constructor
    }
}