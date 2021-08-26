using System.Collections.Generic;
using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Common.Config
{
    public class ConfigSettings
    {
        [JsonProperty("vip")]
        public Vip Vip { get; set; }

        [JsonProperty("sofasplit")]
        public bool SofaSplit { get; set; }

        [JsonProperty("publications")]
        public Publication[] Publications { get; set; }
    }

    public class Vip
    {
        [JsonProperty("names")]
        public string Name { get; set; }

        [JsonProperty("saleStates")]
        public List<SaleState> SaleStates { get; set; }

        [JsonProperty("redirectPage")]
        public PageData RedirectPage { get; set; }

        [JsonProperty("addToBagPage")]
        public PageData AddToBagPage { get; set; }

        [JsonProperty("EnableMultipleAddToBagButtons")]
        public bool EnableMultipleAddToBagButtons { get; set; }

        [JsonProperty("EnableBrandsFacet")]
        public bool EnableBrandsFacet { get; set; }

        [JsonProperty("EnabledEnhancedFacets")]
        public bool EnabledEnhancedFacets { get; set; }

        [JsonProperty("EnabledEnhancedFacetsV2")]
        public bool EnabledEnhancedFacetsV2 { get; set; }

        [JsonProperty("overlayImageSavings")]
        public bool OverlayImageSavings { get; set; }
    }

    public class SaleState
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("publicSliValue")]
        public string PublicSliValue { get; set; }

        [JsonProperty("privateLoginCode")]
        public string PrivateLoginCode { get; set; }

        [JsonProperty("requirePrivate")]
        public bool RequirePrivate { get; set; }

        [JsonProperty("allowAddToBag")]
        public bool AllowAddToBag { get; set; }

        [JsonProperty("pricehistory")]
        public bool PriceHistory { get; set; }

        [JsonProperty("templatePage")]
        public string TemplatePage { get; set; }

        [JsonProperty("isClosed")]
        public bool IsClosed { get; set; }
    }

    public class PageData
    {
        [JsonProperty("source")]
        public string Source { get; set; }
    }

    public class Publication
    {
    }
}