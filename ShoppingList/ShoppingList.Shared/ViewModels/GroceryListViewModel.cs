using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Prism.Commands;
using Prism.Navigation;

using ShoppingList.Shared.Helpers;
using ShoppingList.Shared.Models;
using ShoppingList.Shared.Views;

namespace ShoppingList.Shared.ViewModels
{
    public class GroceryListViewModel : BaseViewModel, IAsyncInitialization, INavigatedAware
    {
        private readonly INavigationService _navigationService;

        public GroceryListViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            AddShoppingListCommand = new DelegateCommand(OnAddShoppingList);

            Initialization = InitializeAsync();
        }

        public Task Initialization { get; }

        public ObservableCollection<GroceryList> GroceryLists { get; private set; }

        public ICommand AddShoppingListCommand { get; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.Count <= 0) return;

            // TODO Push changes to API
            var newGroceryList = new GroceryList { Name = (string)parameters.First().Value };
            GroceryLists.Add(newGroceryList);
            await MockShoppingListDataStore.AddAsync(newGroceryList);
        }

        private async Task InitializeAsync()
        {
            GroceryLists = new ObservableCollection<GroceryList>(await MockShoppingListDataStore.GetAllAsync());
        }

        private async void OnAddShoppingList()
        {
            await _navigationService.NavigateAsync(nameof(GroceryListDetailPage), null, true);
        }
    }
}