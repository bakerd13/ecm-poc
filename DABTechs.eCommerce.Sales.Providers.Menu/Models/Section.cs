using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DABTechs.eCommerce.Sales.Providers.Menu.Models
{
    public class Section
    {
        public string Title { get; set; }

        public List<Link> Links { get; set; }
    }
}
