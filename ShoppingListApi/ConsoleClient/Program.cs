using System;
using IdentityModel.Client;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press any key to continue...\n");
            Console.ReadKey();

            // Discover endpoints from metadata.
            var discoveryResponse = await DiscoveryClient.GetAsync("http://localhost:5000");
            
             if (discoveryResponse.IsError)
            {
                Console.WriteLine(discoveryResponse.Error);
                return;
            }

            // Request token.
            var tokenClient = new TokenClient(discoveryResponse.TokenEndpoint, "shoppingList", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("shoppingListApi");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine();

            // Call API.
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5000/api/ShoppingLists/Test"); // Test URL

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("API Response: " + content);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
