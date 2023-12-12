using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace EDCore
{
    class GraphHelper
    {
        private static Settings _settings;
        private static DeviceCodeCredential _deviceCodeCredential;
        private static GraphServiceClient _userClient;

        public static void InitializeGraphForUserAuth(Settings settings,Func<DeviceCodeInfo, CancellationToken, Task> deviceCodePrompt)
        {
            _settings = settings;

            var options = new DeviceCodeCredentialOptions
            {
                ClientId = settings.ClientId,
                TenantId = settings.TenantId,
                DeviceCodeCallback = deviceCodePrompt,
            };

            _deviceCodeCredential = new DeviceCodeCredential(options);
            _userClient = new GraphServiceClient(_deviceCodeCredential, settings.GraphUserScopes);
        }

        public static async Task<string> GetUserTokenAsync()
        {
            _ = _deviceCodeCredential ??
                throw new System.NullReferenceException("Graph has not been initialized for user auth");
            _ = _settings?.GraphUserScopes ??
                throw new System.ArgumentNullException("Argument 'scopes' cannot be null");

            var context = new TokenRequestContext(_settings.GraphUserScopes);
            var response = await _deviceCodeCredential.GetTokenAsync(context);
            return response.Token;
        }

        public static Task<User> GetUserAsync()
        {
            _ = _userClient ??
                throw new System.NullReferenceException("Graph has not been initialized for user auth");

            return _userClient.Me.GetAsync((config) =>
            {
                config.QueryParameters.Select = new[] { "displayName", "mail", "userPrincipalName" };
            });
        }

#pragma warning disable CS1998
        public async static Task MakeGraphCallAsync()
        {

        }
    }
}
