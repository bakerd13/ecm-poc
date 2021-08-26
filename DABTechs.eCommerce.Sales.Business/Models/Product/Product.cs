using System.Collections.Generic;
using DABTechs.eCommerce.Sales.Interfaces;

namespace DABTechs.eCommerce.Sales.Business.Models.Product
{
    public class Product
    {
        public string Title { get; set; }
        public string PID { get; set; }
        public string Materials { get; set; }
        public double OriginalPrice { get; set; }
        public ICollection<PriceList> PriceHistory { get; set; }
        public double CurrentPrice { get; set; }
        public int Discount { get; set; }
        public string SizesAvailable { get; set; }
        public List<string> SizeList { get; set; }
        public string ImageUrl { get; set; }
    }
}