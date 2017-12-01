using System.Collections.Generic;

namespace ShoppingList.Shared.Models
{
    public class GroceryList
    {
        public GroceryList()
        {
            Items = new List<GroceryItem>();
            Users = new List<User>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<GroceryItem> Items { get; set; }

        public List<User> Users { get; set; }
    }
}