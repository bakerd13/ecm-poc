using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DABTechs.eCommerce.Sales.Providers.Menu.Models
{
    public class Department
    {
        public string Name { get; set; }

        public string SiteUrl { get; set; }

        public List<SearchLink> SearchLinks { get; set; } = new List<SearchLink>();
    }
}
