using System.Collections.Generic;
using DABTechs.eCommerce.Sales.Providers.Azure.Models;

namespace DABTechs.eCommerce.Sales.Providers.Azure.Interfaces
{
    /// <summary>
    /// The Facet Interface.
    /// </summary>
    public interface IFacet
    {
        /// <summary>
        /// Gets or sets the facet elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        List<FacetElement> Elements { get; set; }

        /// <summary>
        /// Gets or sets the facet name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }
    }
}