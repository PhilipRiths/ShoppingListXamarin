using System.Collections.ObjectModel;

using ShoppingList.Shared.Models;

namespace ShoppingList.Shared.ViewModels
{
    public class GroceryItemDetailViewModel : BaseViewModel
    {
        public ObservableCollection<GroceryItem> Items { get; set; }
    }
}