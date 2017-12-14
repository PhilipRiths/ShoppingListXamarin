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
        public GroceryList GroceryList { get; set; }
        public GroceryItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Item = new ObservableCollection<GroceryItem>();
            NewItemCommand = new DelegateCommand(OnCreateItem);
        }

        private async void OnCreateItem()
        {
            var navParams = new NavigationParameters { { Title = "GroceryList", GroceryList } };
            await _navigationService.NavigateAsync($"{nameof(GroceryItemDetailPage)}", navParams, true);
        }

        public ICommand NewItemCommand { get; }


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