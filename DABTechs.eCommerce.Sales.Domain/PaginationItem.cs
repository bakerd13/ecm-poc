namespace DABTechs.eCommerce.Sales.Domain
{
    /// <summary>
    /// The Pagination Item.
    /// </summary>
    public class PaginationItem
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginationItem"/> class.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="isSelectedValue">if set to <c>true</c> [is selected value].</param>
        public PaginationItem(int? pageNumber = null, bool isSelectedValue = false)
        {
            PageNumber = pageNumber;
            IsSelectedValue = isSelectedValue;
            Type = pageNumber != null ? PaginationItemType.Number : PaginationItemType.Ellipsis;
        }

        #endregion Constructor

        /// <summary>
        /// Gets the page number.
        /// </summary>
        /// <value>
        /// The page number.
        /// </value>
        public int? PageNumber { get; }

        /// <summary>
        /// Gets the pagination type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public PaginationItemType Type { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is selected value; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelectedValue { get; }

        /// <summary>
        /// Gets the page title.
        /// </summary>
        /// <value>
        /// The page title.
        /// </value>
        public string PageTitle
        {
            get
            {
                return $"Page { PageNumber }";
            }
        }
    }
}