using System.Collections.ObjectModel;

namespace ShoppingList.Shared.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ObservableCollection<Items> Items { get; set; }
        public ObservableCollection<Users> Users { get; set; }
    }
}