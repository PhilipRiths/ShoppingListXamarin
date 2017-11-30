using System.Windows.Input;

using Prism.Commands;
using Prism.Navigation;

using ShoppingList.Shared.Views;

namespace ShoppingList.Shared.ViewModels
{
    public class GroceryListViewModel
    {
        private readonly INavigationService _navigationService;

        public GroceryListViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            AddShoppingListCommand = new DelegateCommand(OnAddShoppingList);
        }

        public ICommand AddShoppingListCommand { get; }

        private void OnAddShoppingList()
        {
            _navigationService.NavigateAsync(nameof(GroceryListDetailPage), null, true);
        }
    }
}