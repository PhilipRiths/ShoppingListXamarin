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
    public class GroceryItemDetailViewModel : BaseViewModel, INavigatingAware
    {
        private INavigationService _navigationService;
        public ObservableCollection<GroceryItem> Items { get; set; }
        public GroceryList GroceryList { get; set; }
        public GroceryItem GroceryItem { get; set; }
        public MockGroceryListDataStore Store { get; set; }

        public GroceryItemDetailViewModel(INavigationService navigationService)
        {
            Store = new MockGroceryListDataStore();
            GroceryItem = new GroceryItem();
            _navigationService = navigationService;
            SaveCommand = new DelegateCommand(OnSaveExecute);
            CancelCommand = new DelegateCommand(OnCancelExecute);
        }

        private async void OnCancelExecute()
        {
            await _navigationService.GoBackAsync();
        }

        public ICommand CancelCommand { get; set; }

        private async void OnSaveExecute()
        {
            var newGroceryItem = new GroceryItem();
            newGroceryItem.Name = GroceryItem.Name;
            GroceryList.Items.Add(newGroceryItem);
            await Store.UpdateAsync(GroceryList);
            var navParams = new NavigationParameters { { Title = "ItemList", GroceryList } };
            await _navigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(GroceryItemPage)}", navParams, true);


        }
        
        public ICommand SaveCommand { get; set; }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            var groceryList = parameters["GroceryList"] as GroceryList;
            GroceryList = groceryList;
        }
    }
}