using AzureSQLDBCreator.TokenServices;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.Sql.Fluent;
using Microsoft.Azure.Management.Sql.Fluent.Models;
using System.Threading.Tasks;

namespace AzureSQLDBCreator
{
    public class AzureSqlManager
    {
        private readonly SqlManagementClient client;
        private readonly string azureResourceGroup;
        private readonly string azureSQLServer;

        public AzureSqlManager(string resourceGroup, string sqlServer)
        {
            azureResourceGroup = resourceGroup;
            azureSQLServer = sqlServer;


            var azureCreds = AzureTokenService.GetAzureCredentials();
            var restClient = RestClient.Configure()
                                       .WithEnvironment(AzureEnvironment.AzureGlobalCloud)
                                       .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                                       .WithCredentials(azureCreds).Build();

            client = new SqlManagementClient(restClient)
            {
                SubscriptionId = azureCreds.DefaultSubscriptionId
            };
        }

        public async Task<bool> CreateDatabaseAsync(string dbName, string elasticPool, string location,
                                                    CreateMode createMode, DatabaseEdition databaseEdition,
                                                    string collation = "SQL_Latin1_General_CP1_CI_AS")
        {
            var dbInner = new DatabaseInner
            {
                ElasticPoolName = elasticPool,
                Location = location,
                Collation = collation,
                CreateMode = createMode,
                Edition = databaseEdition,
            };

            var resultDbInner = await client.Databases.CreateOrUpdateAsync(azureResourceGroup, azureSQLServer, dbName, dbInner);
            return string.IsNullOrEmpty(resultDbInner.Name) ? false : true;
        }
    }
}
