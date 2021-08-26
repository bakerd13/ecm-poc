using System.Collections.Generic;
using System.Linq;
using DABTechs.eCommerce.Sales.Providers.Azure.Interfaces;

namespace DABTechs.eCommerce.Sales.Providers.Azure.Models
{
    public class Facet : IFacet
    {
        public List<FacetElement> Elements { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }

        public double MinFacetValue()
        {
            List<double> results = new List<double>();

            if (Elements != null)
            {
                foreach (FacetElement facetElement in Elements)
                {
                    if (double.TryParse(facetElement.Value, out double _result))
                    {
                        results.Add(_result);
                    }
                }
            }

            return results.Min();
        }

        public double MaxFacetValue()
        {
            List<double> results = new List<double>();

            if (Elements != null)
            {
                foreach (FacetElement facetElement in this.Elements)
                {
                    if (double.TryParse(facetElement.Value, out double _result))
                    {
                        results.Add(_result);
                    }
                }
            }

            return results.Max();
        }
    }
}