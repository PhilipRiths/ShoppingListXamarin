using Microsoft.EntityFrameworkCore;
using ShoppingListApi.Models;

namespace ShoppingListApi.Data
{
    public class ShoppingListDbContext : DbContext
    {
        public ShoppingListDbContext()
        {
        }

        public DbSet<ShoppingList> ShoppingLists { get; set; }
    }
}