using Azure.Identity;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDCore
{
    public class MainClass
    {
        public bool CrateClient(string myTenantId, string myClientId)
        {
            bool result;
            try
            {
                var scopes = new[] { "User.Read" };

                // Multi-tenant apps can use "common",
                // single-tenant apps must use the tenant ID from the Azure portal
                var tenantId = myTenantId;

                // Value from app registration
                var clientId = myClientId;

                // using Azure.Identity;
                var options = new DeviceCodeCredentialOptions
                {
                    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
                    ClientId = clientId,
                    TenantId = tenantId,
                    // Callback function that receives the user prompt
                    // Prompt contains the generated device code that user must
                    // enter during the auth process in the browser
                    DeviceCodeCallback = (code, cancellation) =>
                    {
                        Console.WriteLine(code.Message);
                        return Task.FromResult(0);
                    },
                };

                // https://learn.microsoft.com/dotnet/api/azure.identity.devicecodecredential
                var deviceCodeCredential = new DeviceCodeCredential(options);

                var graphClient = new GraphServiceClient(deviceCodeCredential, scopes);

                result = true;
            }
            catch (Exception)
            {
                result = false;
                throw;
            }
            return result;
        }
    }
}
