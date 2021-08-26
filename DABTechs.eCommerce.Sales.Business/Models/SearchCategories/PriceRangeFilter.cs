namespace DABTechs.eCommerce.Sales.Business.Models.SearchCategories
{
    public class PriceRangeFilter
    {
        /// <summary>Minimum price of an item in the entire search result</summary>
        public double Min { get; set; }

        /// <summary>Maximum price of an item in the entire search result</summary>
        public double Max { get; set; }

        public override string ToString() => string.Concat("£", Min, " - £", Max);
    }
}