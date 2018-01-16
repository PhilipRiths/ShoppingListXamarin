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
        public ObservableCollection<GroceryItem> Item { get; set; }
        public ObservableCollection<GroceryItem> ItemsInBasket { get; set; }
        public GroceryList GroceryList { get; set; }
        public GroceryItem GroceryItem { get; set; }
        public ICommand MoveItemFromBasket { get; set; }
        public ICommand RemoveItemFromBasket { get; set; }
        public ICommand SaveCommand { get; set; }
        public GroceryItemViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            Item = new ObservableCollection<GroceryItem>();
            ItemsInBasket = new ObservableCollection<GroceryItem>();
            NewItemCommand = new DelegateCommand(OnCreateItem);
            MoveItemToBasket = new DelegateCommand<GroceryItem>(OnMoveItemToBasket);
            MoveItemFromBasket = new DelegateCommand<GroceryItem>(OnMoveItemFromBasket);
            RemoveItemFromBasket = new DelegateCommand<GroceryItem>(OnDelete);
            SaveCommand = new DelegateCommand(OnSaveExecute);
            GroceryItem = new GroceryItem();
            RemoveItemCommand = new DelegateCommand<GroceryItem>(OnRemoveGroceryListItemExecute);
            EditItemCommand = new DelegateCommand<GroceryItem>(OnEditGroceryListItemExecute);
        }

        private void OnEditGroceryListItemExecute(GroceryItem obj)
        {
            var navigationParameters = new NavigationParameters{ { "GroceryListItem", obj } };
            _navigationService.NavigateAsync(nameof(GroceryItemDetailPage), navigationParameters);
        }


        private async void OnRemoveGroceryListItemExecute(GroceryItem item)
        {
            var result = await _dialogService.DisplayAlertAsync("", 
                "Are you sure you want to delete this item?", 
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
            Item.Add(newGroceryItem);
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
            await _navigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(GroceryItemDetailPage)}", navParams, true);
        }

        public ICommand NewItemCommand { get; }
        public ICommand MoveItemToBasket { get; set; }
        public DelegateCommand<GroceryItem> RemoveItemCommand { get; }
        public DelegateCommand<GroceryItem> EditItemCommand { get; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            var groceryList = parameters["ItemList"] as GroceryList;
            GroceryList = groceryList;
            foreach (var item in groceryList.Items)
            {
                Item.Add(item);
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }
    }
}