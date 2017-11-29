using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ShoppingList.Shared.Models;
using ShoppingList.Shared.Views;
using Xamarin.Forms;

namespace ShoppingList.Shared.ViewModels
{
    public class ListDetailViewModel : BaseViewModel
    {
        public ObservableCollection<Items> Items { get; set; }

        public ListDetailViewModel(ShoppingLists shoppingList = null)
        {
            Title = shoppingList?.Name;
            Items = shoppingList?.Items;

       
        }

    }
}
