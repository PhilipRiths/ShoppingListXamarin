using IdentityServer4.Models;
using System.Collections.Generic;

namespace ShoppingListApi
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("shoppingListApi", "Shopping list API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                   ClientId = "android",
                   ClientName = "Android",
                    
                    // No interactive user, use the clientid/secret for authentication.
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    
                    // Secret for authentication
                    ClientSecrets =
                    {
                        new Secret("androidSecret".Sha256())
                    },
                    


                    // Scopes that client has access to
                    AllowedScopes = { "shoppingListApi" }
                },

                new Client
                {
                   ClientId = "ios",
                    
                    // No interactive user, use the clientid/secret for authentication.
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    
                    // Secret for authentication
                    ClientSecrets =
                    {
                        new Secret("iosSecret".Sha256())
                    },

                    // Scopes that client has access to
                    AllowedScopes = { "shoppingListApi" }
                }
            };
        }
    }
}