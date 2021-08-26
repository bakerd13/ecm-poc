using Microsoft.Azure.Cosmos;
using DABTechs.eCommerce.Sales.Business.Interfaces;
using DABTechs.eCommerce.Sales.Business.Models.ShoppingBag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DABTechs.eCommerce.Sales.Providers.CosmosDb
{
    public class ShoppingBagPersistence : IShoppingBagPersistence
    {
        private readonly string _databaseId;
        private readonly string _endPoint;
        private readonly string _primaryKey;
        private CosmosClient _cosmosClient;
        private string _containerId;
        private Database _database;
        private Container _container;

        public ShoppingBagPersistence(string endPoint, string primaryKey)
        {
            // TODO put into config
            _databaseId = "VipCosmosDB";
            _containerId = "ShoppingBag";

            _endPoint = endPoint;
            _primaryKey = primaryKey;
        }

        public async Task EnsureSetupAsync()
        {
            if (_cosmosClient == null)
            {
                var options = new CosmosClientOptions();

                // TODO should we need this as this is dev mode and getting it working
                options.ConnectionMode = ConnectionMode.Gateway;
                var byPassList = new List<string>();
                byPassList.Add("172.20.218.217");
                byPassList.Add("127.0.0.1");

                options.WebProxy = new WebProxy
                {
                    BypassProxyOnLocal = false,
                    BypassList = byPassList.ToArray(),
                };

                _cosmosClient = new CosmosClient(_endPoint, _primaryKey, options);
            }

            _database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseId);

            // Need to know what the partionKey Is
            _container = await _database.CreateContainerIfNotExistsAsync(_containerId, "/Id");
        }

        public async Task<Bag> CreateShoppingBagAsync(Bag bag)
        {
            await EnsureSetupAsync();

            try
            {
                ItemResponse<Bag> response = await this._container.CreateItemAsync<Bag>(bag);

            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
            {
                Console.WriteLine("Item in database with id: {0} already exists\n", bag.Id);
            }

            // TODO what is the Id
            return bag;
        }

        public async Task<Bag> GetShoppingBagAsync(string id)
        {
            await EnsureSetupAsync();

            QueryDefinition queryDefinition = new QueryDefinition($"SELECT * FROM c WHERE c.id = {id}");
            FeedIterator<Bag> queryResultSetIterator = this._container.GetItemQueryIterator<Bag>(queryDefinition);

            List<Bag> bags = new List<Bag>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Bag> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Bag bag in currentResultSet)
                {
                    bags.Add(bag);
                }
            }

            return bags.FirstOrDefault(); ;
        }
    }
}

