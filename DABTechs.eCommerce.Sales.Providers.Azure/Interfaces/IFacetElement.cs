namespace DABTechs.eCommerce.Sales.Providers.Azure.Interfaces
{
    public interface IFacetElement
    {
        /// <summary>
        /// Gets or sets the facet value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        string Value { get; set; }

        /// <summary>
        /// Gets or sets the facet count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        int Count { get; set; }
    }
}