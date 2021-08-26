using System.Collections.Generic;
using System.Xml.Serialization;

namespace DABTechs.eCommerce.Sales.Providers.Menu.Models
{
    //[XmlRoot("meganav", Namespace= "http://www.next.co.uk")]
    public class MegaNav
    {
        //[XmlElement("departments")]
        public List<Department> Departments { get; set; } = new List<Department>();
    }
}
