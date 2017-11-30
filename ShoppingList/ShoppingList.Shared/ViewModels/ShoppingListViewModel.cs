﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using ShoppingList.Shared.Models;
using ShoppingList.Shared.Views;
using Xamarin.Forms;

namespace ShoppingList.Shared.ViewModels
{
    public class ShoppingListViewModel : BaseViewModel
    {
        public ObservableCollection<Items> Items { get; set; }
        public ObservableCollection<ShoppingLists> ShoppingLists { get; set; }
        public ICommand LoadShoppingListCommand { get; set; }
        public ShoppingLists ShoppingList { get; set; }
        public ShoppingListViewModel(ShoppingLists shoppingList = null)
        {
            Title = shoppingList?.Name;
            Items = shoppingList?.Items;
            //Title = "Shoppinglists";
            ShoppingList = shoppingList;
            ShoppingLists = new ObservableCollection<ShoppingLists>();
            LoadShoppingListCommand = new DelegateCommand(async () => await ExecuteLoadShoppingListCommand());
            AddShoppingListCommand = new DelegateCommand(AddShoppingListExecute);

            MessagingCenter.Subscribe<NewListPage, ShoppingLists>(this, "AddList", async (obj, list) =>
            {
                var _list = list as ShoppingLists;
                ShoppingLists.Add(_list);
                await ShoppingListDataStore.AddAsync(_list);
            });
            MessagingCenter.Subscribe<NewItemPage, ShoppingLists>(this, "UpdateList", async (obj, list) =>
            {
                var _list = list as ShoppingLists;
                ShoppingLists.Remove(_list);
                ShoppingLists.Add(_list);
                await ShoppingListDataStore.UpdateAsync(_list);
            });

        }

        public ICommand AddShoppingListCommand { get; }

        private void AddShoppingListExecute()
        {
            throw new System.NotImplementedException();
        }
        async Task ExecuteLoadShoppingListCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ShoppingLists.Clear();
                var shoppingLists = await ShoppingListDataStore.GetAllAsync(true);
                foreach (var list in shoppingLists)
                {
                    ShoppingLists.Add(list);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
