using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private bool _isValidItemQuantityEntry;

        public MockGroceryListDataStore Store { get; set; }

        public GroceryItemDetailViewModel(INavigationService navigationService)
        {
            Store = new MockGroceryListDataStore();
            _navigationService = navigationService;
            SaveCommand = new DelegateCommand(OnSaveExecute);
            CancelCommand = new DelegateCommand(OnCancelExecute);
        }

        private async void OnCancelExecute()
        {
            await _navigationService.GoBackAsync();
        }

        public ICommand CancelCommand { get; set; }

        public bool IsValidEntry
        {
            get { return _isValidEntry; }
            set
            {
                _isValidEntry = value;
                RaisePropertyChanged();
            }
        }

        public bool IsValidItemQuantityEntry
        {
            get { return _isValidItemQuantityEntry; }
            set
            {
                if (ItemQuantity > 0)
                {
                    _isValidItemQuantityEntry = true;
                }
                else
                {
                    _isValidItemQuantityEntry = false;
                }
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

        private async void OnSaveExecute()
        {
            //TODO: This method should save the current editable grocery item
            GroceryItem.Name = ItemName;
            GroceryItem.Quantity = ItemQuantity;
            var navParams = new NavigationParameters{{"ItemsList", GroceryList}};
            await _navigationService.GoBackAsync(navParams);
        }
        
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

            var itemsList = parameters["ObservableItemsList"] as ObservableCollection<GroceryItem>;
            Items = itemsList;

            var groceryList = parameters["ItemsList"] as GroceryList;
            GroceryList = groceryList;
        }
    }
}