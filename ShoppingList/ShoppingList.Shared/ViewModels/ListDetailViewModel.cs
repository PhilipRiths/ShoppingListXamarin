using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ShoppingList.Shared.Models;
using ShoppingList.Shared.Views;
using Xamarin.Forms;

namespace ShoppingList.Shared.ViewModels
{
    public class ListDetailViewModel : BaseViewModel
    {
        public ObservableCollection<Items> Items { get; set; }
        private ShoppingListViewModel _shoppingListViewModel;
        public ListDetailViewModel(ShoppingLists shoppingList = null)
        {
            Title = shoppingList?.Name;
            Items = shoppingList?.Items;
            
            MessagingCenter.Subscribe<NewItemPage, Items>(this, "AddList", async (obj, items) =>
            {
                var _item = items as Items;
                var findList = _shoppingListViewModel.ShoppingLists.FirstOrDefault(sl => sl.Id == shoppingList.Id);
                Items.Add(_item);
                findList.Items.Add(_item);
                await ShoppingListDataStore.AddAsync(findList);
            });
        }

    }
}
