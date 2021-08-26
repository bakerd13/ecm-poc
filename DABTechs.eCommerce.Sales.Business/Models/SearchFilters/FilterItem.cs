namespace DABTechs.eCommerce.Sales.Business.Models.SearchFilters
{
    public class FilterItem
    {
        public string FacetName { get; set; }
        public string Value { get; set; }

        public string Title { get; set; }

        public bool Selected { get; set; }

        public int Count { get; set; }

        public virtual string GetCount() => string.Concat('(', Count, ')');

        public override string ToString()
        {
            return FacetName;
        }
    }
}