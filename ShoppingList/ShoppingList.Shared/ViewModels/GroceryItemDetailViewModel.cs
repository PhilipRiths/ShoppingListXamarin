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
        public ObservableCollection<GroceryItem> Items { get; set; }
        public GroceryList GroceryList { get; set; }



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

        public bool IsValidEntry
        {
            get { return _isValidEntry; }
            set
            {
                _isValidEntry = value;
                RaisePropertyChanged();
            }
        }


        private void OnSaveExecute()
        {
            //TODO: This method should save the current editable grocery item


            //var newGroceryItem = new GroceryItem();
            //newGroceryItem.Name = GroceryItem.Name;
            //GroceryList.Items.Add(newGroceryItem);
            //await Store.UpdateAsync(GroceryList);
            //var navParams = new NavigationParameters { { Title = "ItemList", GroceryList } };
            //await _navigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(GroceryItemPage)}", navParams, true);
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
            var groceryList = parameters["GroceryList"] as GroceryList;
            GroceryList = groceryList;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            var groceryItem = parameters["GroceryListItem"] as GroceryItem;
            GroceryItem = groceryItem;
        }
    }
}