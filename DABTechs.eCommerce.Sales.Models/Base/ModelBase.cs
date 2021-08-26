using DABTechs.eCommerce.Sales.Common.Config;

namespace DABTechs.eCommerce.Sales.Models.Base
{
    public class ModelBase
    {
        public string Message { get; set; }

        public int Status { get; set; }

        public string HtmlContent { get; set; }

        public string LocalResource
        {
            get
            {
                return _appSettings.LocalResource;
            }
        }

        public string LocalServer
        {
            get
            {
                return _appSettings.LocalServer;
            }
        }

        public string RemoteServer
        {
            get
            {
                return _appSettings.RemoteServer;
            }
        }

        public string RemoteDomain
        {
            get
            {
                return _appSettings.RemoteDomain;
            }
        }

        private readonly AppSettings _appSettings;

        public ModelBase()
        {
            this.Status = 200;
        }

        public ModelBase(AppSettings appSettings) : this()
        {
            _appSettings = appSettings;
        }
    }
}