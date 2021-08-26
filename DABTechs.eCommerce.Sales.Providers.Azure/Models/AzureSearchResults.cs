using System;
using System.Collections.Generic;
using System.Linq;
using DABTechs.eCommerce.Sales.Providers.Azure.Interfaces;

namespace DABTechs.eCommerce.Sales.Providers.Azure.Models
{
    public class AzureSearchResults : ISearchResults
    {
        /// <summary>
        /// Gets or sets the facets.
        /// </summary>
        /// <value>
        /// The facets.
        /// </value>
        public List<Facet> Facets { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public List<Item> Items { get; set; }

        /// <summary>
        /// Gets or sets the total results.
        /// </summary>
        /// <value>
        /// The total results.
        /// </value>
        public int TotalResults { get; set; }

        /// <summary>
        /// Gets or sets the total pages.
        /// </summary>
        /// <value>
        /// The total pages.
        /// </value>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>
        /// The current page.
        /// </value>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        public DateTime? LastUpdated { get; set; }

        /// <summary>
        /// Maps the specified results.
        /// </summary>
        /// <param name="results">The results.</param>
        public void Map(ISearchResults results)
        {
            if (results == null) { return; }

            Facets = results.Facets;
            Items = results.Items;
            TotalResults = results.TotalResults;
            TotalPages = results.TotalPages;
            CurrentPage = results.CurrentPage;
            Message = results.Message;
        }

        public Facet this[string name]
        {
            get
            {
                return Facets.FirstOrDefault(f => string.Equals(f.Name, name, StringComparison.InvariantCultureIgnoreCase));
            }
        }
    }
}