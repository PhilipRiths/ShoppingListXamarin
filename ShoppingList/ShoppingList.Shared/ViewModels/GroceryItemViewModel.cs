using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using ShoppingList.Shared.Models;
using ShoppingList.Shared.Views;
using Xamarin.Forms;

namespace ShoppingList.Shared.ViewModels
{
    public class GroceryItemViewModel : BaseViewModel, INavigationAware
    {
        private INavigationService _navigationService;
        public ObservableCollection<GroceryItem> Item { get; set; }
        public ObservableCollection<GroceryItem> ItemsInBasket { get; set; }
        public GroceryList GroceryList { get; set; }
        public ICommand MoveItemFromBasket { get; set; }
        public ICommand RemoveItemFromBasket { get; set; }
        public GroceryItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Item = new ObservableCollection<GroceryItem>();
            ItemsInBasket = new ObservableCollection<GroceryItem>();
            NewItemCommand = new DelegateCommand(OnCreateItem);
            MoveItemToBasket = new DelegateCommand<GroceryItem>(OnMoveItemToBasket);
            MoveItemFromBasket = new DelegateCommand<GroceryItem>(OnMoveItemFromBasket);
            RemoveItemFromBasket = new DelegateCommand<GroceryItem>(OnDelete);
            RemoveItemCommand = new DelegateCommand<GroceryItem>(OnRemoveGroceryListItemExecute);

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

        private void OnRemoveGroceryListItemExecute(GroceryItem item)
        {
            GroceryList.Items.Remove(item);
            Item.Remove(item);
            //TODO: Update API async
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

        public ICommand NewItemCommand { get; }

        public ICommand MoveItemToBasket { get; set; }

        public ICommand RemoveItemCommand { get; }

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