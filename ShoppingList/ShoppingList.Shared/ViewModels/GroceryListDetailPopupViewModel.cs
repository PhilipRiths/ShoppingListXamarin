using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

using ShoppingList.Shared.Models;

namespace ShoppingList.Shared.ViewModels
{
    public class GroceryListDetailPopupViewModel : BaseViewModel, INavigatingAware
    {
        private readonly IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private GroceryList _groceryList;

        public GroceryListDetailPopupViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            CancelCommand = new DelegateCommand(OnCancel);
            SaveCommand = new DelegateCommand(OnSave);
        }

        public DelegateCommand CancelCommand { get; }

        public DelegateCommand SaveCommand { get; }

        public GroceryList GroceryList
        {
            get => _groceryList;
            set => SetProperty(ref _groceryList, value);
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            GroceryList = parameters["GroceryList"] as GroceryList;
        }

        private async void OnCancel()
        {
            await _navigationService.GoBackAsync();
        }

        private async void OnSave()
        {
            await _dialogService.DisplayAlertAsync(string.Empty, "Your grocery list was saved!", "OK");

            await _navigationService.GoBackAsync(new NavigationParameters { { "GroceryList", GroceryList } });
        }
    }
}