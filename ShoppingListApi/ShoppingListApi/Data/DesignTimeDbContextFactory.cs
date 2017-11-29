using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ShoppingListApi.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ShoppingListContext>
    {
        public ShoppingListContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ShoppingListContext>();

            var connectionString = configurationRoot.GetConnectionString("shoppingListDBConnectionString");

            builder.UseSqlServer(connectionString);

            return new ShoppingListContext(builder.Options);
        }
    }
}