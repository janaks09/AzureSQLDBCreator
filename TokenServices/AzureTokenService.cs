using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;

namespace AzureSQLDBCreator.TokenServices
{
    public static class AzureTokenService
    {
        public static AzureCredentials GetAzureCredentials()
        {
            var subscriptionId = "[Subscription_Id]";
            var appId = "[App_Id]";
            var appSecret = "[App_Secret]";
            var tenantId = "[Tenant_Id]";
            var environment = AzureEnvironment.AzureGlobalCloud;

            var credentials = new AzureCredentialsFactory().FromServicePrincipal(appId, appSecret, tenantId, environment);

            return credentials.WithDefaultSubscription(subscriptionId);
        }
    }

}
