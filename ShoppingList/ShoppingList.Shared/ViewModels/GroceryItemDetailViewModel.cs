using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using ShoppingList.Shared.Models;
using ShoppingList.Shared.Services;
using ShoppingList.Shared.Views;
using Xamarin.Forms;

namespace ShoppingList.Shared.ViewModels
{
    public class GroceryItemDetailViewModel : BaseViewModel, INavigatingAware, INavigatedAware
    {
        private INavigationService _navigationService;
        private GroceryItem _groceryItem;
        private bool _isValidEntry = false;
        private string _itemName;
        private int _itemQuantity;
        private ObservableCollection<GroceryItem> _items;
        private GroceryList _groceryList;
        private ItemMeasurement _selectedItemMeasurement;

        public MockGroceryListDataStore Store { get; set; }

        public GroceryItemDetailViewModel(INavigationService navigationService)
        {
            Store = new MockGroceryListDataStore();
            _navigationService = navigationService;
            SaveCommand = new DelegateCommand(OnSaveExecute);
            CancelCommand = new DelegateCommand(OnCancelExecute);
        }
        

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            var cachedItemList = new List<GroceryItem>();

            foreach (var item in Items)
            {
                cachedItemList.Add(item);
            }
            Items.Clear();

            foreach (var item in cachedItemList)
            {
                Items.Add(item);
            }

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            var groceryItem = parameters["GroceryListItem"] as GroceryItem;
            GroceryItem = groceryItem;
            ItemName = groceryItem.Name;
            ItemQuantity = groceryItem.Quantity;
            SelectedItemMeasurement = groceryItem.Measurement;

            var itemsList = parameters["ObservableItemsList"] as ObservableCollection<GroceryItem>;
            Items = itemsList;

            var groceryList = parameters["ItemsList"] as GroceryList;
            GroceryList = groceryList;
        }

        public ICommand CancelCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        public GroceryItem GroceryItem
        {
            get { return _groceryItem; }
            set
            {
                _groceryItem = value;
                RaisePropertyChanged();
            }
        }

        public bool IsValidEntry
        {
            get { return _isValidEntry; }
            set
            {
                _isValidEntry = value;
                RaisePropertyChanged();
            }
        }

        public string ItemName
        {
            get { return _itemName; }
            set
            {
                _itemName = value;
                RaisePropertyChanged();
            }
        }

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                RaisePropertyChanged();
            }
        }

        public ItemMeasurement SelectedItemMeasurement
        {
            get { return _selectedItemMeasurement; }
            set
            {
                _selectedItemMeasurement = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<GroceryItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged();
            }
        }

        public GroceryList GroceryList
        {
            get { return _groceryList; }
            set
            {
                _groceryList = value;
                RaisePropertyChanged();
            }
        }

        public List<string> ItemMeasurements => Enum.GetNames(typeof(ItemMeasurement)).ToList();


        private async void OnSaveExecute()
        {
            GroceryItem.Name = ItemName;
            GroceryItem.Quantity = ItemQuantity;
            GroceryItem.Measurement = SelectedItemMeasurement;
            var navParams = new NavigationParameters { { "ItemsList", GroceryList } };
            await _navigationService.GoBackAsync(navParams);
        }

        private async void OnCancelExecute()
        {
            await _navigationService.GoBackAsync();
        }
    }
}