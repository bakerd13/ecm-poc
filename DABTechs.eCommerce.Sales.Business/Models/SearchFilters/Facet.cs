using DABTechs.eCommerce.Sales.Interfaces;
using System.Collections.Generic;

namespace DABTechs.eCommerce.Sales.Models.SearchFilters
{
    /// <summary>
    /// The Facet.
    /// </summary>
    public class Facet
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        public List<FacetElement> Elements { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}