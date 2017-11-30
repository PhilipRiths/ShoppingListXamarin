using System.Collections.ObjectModel;

namespace ShoppingList.Shared.Models
{
    public class GroceryList
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ObservableCollection<GroceryItem> Items { get; set; }

        public ObservableCollection<User> Users { get; set; }
    }
}