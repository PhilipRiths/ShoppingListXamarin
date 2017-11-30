using System.Collections.ObjectModel;

using ShoppingList.Shared.Models;

namespace ShoppingList.Shared.ViewModels
{
    public class GroceryItemDetailViewModel : BaseViewModel
    {
        public GroceryItemDetailViewModel(GroceryList groceryList = null)
        {
            Title = groceryList?.Name;
            Items = groceryList?.Items;
        }

        public ObservableCollection<GroceryItem> Items { get; set; }
    }
}