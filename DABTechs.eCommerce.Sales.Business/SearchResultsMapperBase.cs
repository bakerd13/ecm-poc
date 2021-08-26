using System.Collections.Generic;
using System.Linq;
using DABTechs.eCommerce.Sales.Business.Collections;
using DABTechs.eCommerce.Sales.Business.Interfaces;
using DABTechs.eCommerce.Sales.Common.Config;

namespace DABTechs.eCommerce.Sales.Business
{
    public abstract class SearchResultsMapperBase : ISearchResultMapper
    {
        private const int BloomReachResultsLimit = 10000;
        protected readonly AppSettings _appSettings;

        protected SearchResultsMapperBase(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public abstract SearchResults Map(string rawResult);

        public string ImageUri
        {
            get
            {
                return _appSettings.ImageBaseUrl;
            }
        }

        public string GiveProductImageUrl(string pid)
        {
            if (string.IsNullOrWhiteSpace(pid)) { return string.Empty; }
            string formattedPid = pid.Length > 6 ? $"{pid.Insert(6, "-")}" : pid;
            return $"{ImageUri}{formattedPid}.jpg";
        }


        /// <summary>This will calculate total Page required for the Numberof Items found. if 25 items found then 2 Pages will be required</summary>
        /// <param name="totalSearchResultItems"></param>
        /// <returns>Total Pages Required</returns>
        private int GetTotalPageCount(int totalSearchResultItems)
        {
            if (totalSearchResultItems <= 0) { return 0; }
            int ItemsPerPage = _appSettings.ProductsPerPage;
            if (totalSearchResultItems % ItemsPerPage > 0) return (totalSearchResultItems / ItemsPerPage) + 1;    //## 39 Matching Results found, page size 24. So- TotalPages required= 2
            if (totalSearchResultItems < ItemsPerPage) return 1;                                                //## 20 Matching Results found, page size 24. So- TotalPage required = 1
            return (totalSearchResultItems / ItemsPerPage); //## 24 or 48 or 96 Total records found, divisable by 24 exactly! Hurray!!
        }

        public static List<List<ProductItemCollection>> GiveGroupedProductItems(ICollection<ProductItemCollection> productItems)
        {
            var groups = new List<List<ProductItemCollection>>();

            var productItemCollections = productItems.ToList();

            var groupsCount = productItemCollections.Count / 2;

            if (productItemCollections.Count % 2 > 0)
                groupsCount++;

            for (var i = 0; i < groupsCount; i++)
                groups.Add(productItemCollections.Skip(i * 2).Take(2).ToList());

            return groups;
        }
    }
}