using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using ShoppingList.Shared.Models;
using ShoppingList.Shared.Views;
using Xamarin.Forms;

namespace ShoppingList.Shared.ViewModels
{
    public class GroceryItemViewModel : BaseViewModel, INavigationAware
    {
        private INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private GroceryList _groceryList;
        private GroceryItem _groceryItem;
        private ObservableCollection<GroceryItem> _item;

        
        public GroceryItemViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _groceryItem = new GroceryItem();
            Item = new ObservableCollection<GroceryItem>();
            ItemsInBasket = new ObservableCollection<GroceryItem>();
            NewItemCommand = new DelegateCommand(OnCreateItem);
            MoveItemToBasket = new DelegateCommand<GroceryItem>(OnMoveItemToBasket);
            MoveItemFromBasket = new DelegateCommand<GroceryItem>(OnMoveItemFromBasket);
            RemoveItemFromBasket = new DelegateCommand<GroceryItem>(OnDelete);
            SaveCommand = new DelegateCommand(OnSaveExecute);
            RemoveItemCommand = new DelegateCommand<GroceryItem>(OnRemoveGroceryListItemExecute);
            EditItemCommand = new DelegateCommand<GroceryItem>(OnEditGroceryListItemExecute);
        }

        private void OnEditGroceryListItemExecute(GroceryItem obj)
        {
            var navigationParameters = new NavigationParameters{ { "GroceryListItem", obj }, {"ItemsList", GroceryList}, {"ObservableItemsList", Item} };
            _navigationService.NavigateAsync(nameof(GroceryItemDetailPage), navigationParameters);
        }


        private async void OnRemoveGroceryListItemExecute(GroceryItem item)
        {
            var result = await _dialogService.DisplayAlertAsync("", 
                $"Are you sure you want to delete the item {item.Name}", 
                "OK",
                "Cancel");
            if (result)
            {
                GroceryList.Items.Remove(item);
                Item.Remove(item);
            }
            //TODO: Update API async
        }

        private void OnSaveExecute()
        {
            var newGroceryItem = new GroceryItem();
            newGroceryItem.Name = GroceryItem.Name;
            newGroceryItem.Quantity = 1;
            Item.Add(newGroceryItem);
            GroceryList.Items.Add(newGroceryItem);
            //TODO: Update API async
        }

        private void OnDelete(GroceryItem item)
        {
            ItemsInBasket.Remove(item);
        }

        private void OnMoveItemFromBasket(GroceryItem item)
        {
            ItemsInBasket.Remove(item);
            Item.Add(item);
        }

        private void OnMoveItemToBasket(GroceryItem item)
        {
            Item.Remove(item);
            ItemsInBasket.Add(item);
        }

        private async void OnCreateItem()
        {
            var navParams = new NavigationParameters { { Title = "GroceryList", GroceryList } };
            await _navigationService.NavigateAsync($"{nameof(GroceryItemDetailPage)}", navParams, true);
        }
        public ObservableCollection<GroceryItem> Item
        {
            get { return _item; }
            set
            {
                _item = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<GroceryItem> ItemsInBasket { get; set; }

        public GroceryList GroceryList
        {
            get { return _groceryList; }
            set
            {
                _groceryList = value;
                RaisePropertyChanged();
            }
        }

        public GroceryItem GroceryItem
        {
            get { return _groceryItem; }
            set
            {
                _groceryItem = value;
                RaisePropertyChanged();
            }
        }

        public ICommand MoveItemFromBasket { get; set; }
        public ICommand RemoveItemFromBasket { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand NewItemCommand { get; }
        public ICommand MoveItemToBasket { get; set; }
        public DelegateCommand<GroceryItem> RemoveItemCommand { get; }
        public DelegateCommand<GroceryItem> EditItemCommand { get; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (GroceryList == null)
            {
                var groceryList = parameters["ItemList"] as GroceryList;
                GroceryList = groceryList;
                foreach (var item in groceryList.Items)
                {
                    Item.Add(item);
                }
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            if (GroceryList == null)
            {
                var groceryList = parameters["ItemsList"] as GroceryList;
                GroceryList = groceryList;
            }
        }
    }
}