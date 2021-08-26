using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using DABTechs.eCommerce.Sales.Business.Interfaces;
using DABTechs.eCommerce.Sales.Common.Config;
using DABTechs.eCommerce.Sales.Common.Enums;
using DABTechs.eCommerce.Sales.Providers.Azure;

namespace DABTechs.eCommerce.Sales.UnitTests
{
    [TestClass]
    public class ServiceProviderTests
    {
        private const string SettingsFilePath = @"Data\appsettings.test.json";
        private static IOptions<AppSettings> AppSettingsOptions;

        [ClassInitialize]
        public static void Init(TestContext _)
        {
            if (!File.Exists(SettingsFilePath))
            {
                throw new FileNotFoundException($"Cannot find app settings file at '{SettingsFilePath}'");
            }
            var fileContent = File.ReadAllText(SettingsFilePath);
            var appSettings = JsonConvert.DeserializeObject<AppSettings>(fileContent);
            AppSettingsOptions = Options.Create(appSettings);
        }

        //[TestMethod]
        //public void AzureSearch_Map_StarSearch()
        //{
        //    // Arrange
        //    var searchQuery = new SearchQuery
        //    {
        //        W = "*",
        //        ISort = "orderValue desc"
        //    };

        //    var rawResults = GetFileContents<string>("StarSearch.json");
        //    var searchProvider = GetSearchProvider(SearchProvider.Azure);

        //    // Act
        //    var result = JsonConvert.SerializeObject(searchProvider.Map(rawResults, searchQuery));
        //    var expected = GetFileContents<string>("StarSearchExpected.json");

        //    // Assert
        //    Assert.AreEqual(expected, result);
        //}

        [TestMethod]
        public void AzureSearch_SearchProviderType_IsCorrectType()
        {
            // Arrange
            var searchProvider = GetSearchProvider(SearchProvider.Azure, null);

            // Assert
            Assert.AreEqual(SearchProvider.Azure, searchProvider.SearchProviderType);
        }

        private static T GetFileContents<T>(string filename)
        {
            var filePath = Path.Combine("Data", filename);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Cannot find file at '{filePath}'");
            }
            var fileContent = File.ReadAllText(filePath);
            return typeof(T) == typeof(string) ? (T)(object)fileContent : JsonConvert.DeserializeObject<T>(fileContent);
        }

        private ISearchProvider GetSearchProvider(SearchProvider searchProvider, string responseString = "")
        {
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(responseString ?? string.Empty)
               }).Verifiable();

            switch (searchProvider)
            {
                case SearchProvider.Azure:
                    var azureHttpClient = new AzureHttpClient(new HttpClient(httpMessageHandlerMock.Object), AppSettingsOptions);
                    return new AzureSearchProvider(azureHttpClient, AppSettingsOptions);

                default:
                    throw new NotImplementedException($"Search provider: '{searchProvider}' has not been implemented");
            }
        }
    }
}