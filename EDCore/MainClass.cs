using Microsoft.Graph.Models;
using System;
using System.Threading.Tasks;

namespace EDCore
{
    public class MainClass
    {
        public static void InitializeGraphModule()
        {
            var settings = Settings.LoadSettings();
            GraphHelper.InitializeGraphForUserAuth(settings,
                    (info, cancel) =>
                    {
                        Console.WriteLine(info.Message);
                        return Task.FromResult(0);
                    });
        }
        
        public async static Task<User> GetUserInformationAsync()
        {
                var user = await GraphHelper.GetUserAsync();
                Console.WriteLine($"用户名, {user?.DisplayName}");
                Console.WriteLine($"邮箱: {user?.Mail ?? user?.UserPrincipalName ?? ""}");
                return user;
        }
    }
}
