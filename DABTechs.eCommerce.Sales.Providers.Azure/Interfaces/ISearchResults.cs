using System;
using System.Collections.Generic;
using DABTechs.eCommerce.Sales.Providers.Azure.Models;

namespace DABTechs.eCommerce.Sales.Providers.Azure.Interfaces
{
    /// <summary>
    /// The Search Results Interface.
    /// </summary>
    public interface ISearchResults
    {
        /// <summary>
        /// Gets or sets the facets.
        /// </summary>
        /// <value>
        /// The facets.
        /// </value>
        List<Facet> Facets { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        List<Item> Items { get; set; }

        /// <summary>
        /// Gets or sets the total results.
        /// </summary>
        /// <value>
        /// The total results.
        /// </value>
        int TotalResults { get; set; }

        /// <summary>
        /// Gets or sets the total pages.
        /// </summary>
        /// <value>
        /// The total pages.
        /// </value>
        int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>
        /// The current page.
        /// </value>
        int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        string Message { get; set; }

        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        /// <value>
        /// The last updated.
        /// </value>
        DateTime? LastUpdated { get; set; }
    }
}