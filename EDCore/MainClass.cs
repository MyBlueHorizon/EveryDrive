using Azure.Identity;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace EDCore
{
    public class MainClass
    {
        GraphServiceClient graphClient;
        public void CrateClient(string myTenantId, string myClientId)
        {
            
            //权限与租户信息
            var scopes = new[] { "User.Read" };
            var tenantId = myTenantId;
            var clientId = myClientId;

            //身份验证
            var options = new DeviceCodeCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
                ClientId = clientId,
                TenantId = tenantId,
                DeviceCodeCallback = (code, cancellation) =>
                {
                    Console.WriteLine(code.Message);
                    MessageBox.Show(code.Message);
                    return Task.FromResult(0);
                },
            };
            var deviceCodeCredential = new DeviceCodeCredential(options);
            graphClient = new GraphServiceClient(deviceCodeCredential, scopes); 
        }
        public async Task<string> GetUserInformationAsync()
        {
            // GET https://graph.microsoft.com/v1.0/me?$select=displayName
            var user = await graphClient.Me
                .GetAsync(requestConfiguration =>
                {
                    requestConfiguration.QueryParameters.Select =
                        new string[] { "displayName"};
                });
            MessageBox.Show(user.DisplayName);
            return user.DisplayName;
        }
    }
}
