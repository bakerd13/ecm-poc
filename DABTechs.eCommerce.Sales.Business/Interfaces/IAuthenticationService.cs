namespace DABTechs.eCommerce.Sales.Business.Interfaces
{
    public interface IAuthenticationService
    {
        string AccountNo { get; set; }
        string AuthToken { get; set; }
        bool CookieFound { get; set; }
        bool ReadVIPSalePopUpModal { get; set; }
        string SourceSite { get; set; }
        long Timestamp { get; set; }

        bool AuthCheck();

        void ClearCookie();

        void WriteCookie();
    }
}