using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DABTechs.eCommerce.Sales.Providers.Menu.Models
{
    public class SearchLink
    {
        public List<Column> Columns { get; set; } = new List<Column>(); 
    }
}
