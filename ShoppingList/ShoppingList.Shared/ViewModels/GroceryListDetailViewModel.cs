using System.Windows.Input;

using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

using ShoppingList.Shared.Models;

namespace ShoppingList.Shared.ViewModels
{
    public class GroceryListDetailViewModel : BaseViewModel, INavigatedAware
    {
        private readonly IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private GroceryList _groceryList;

        public GroceryListDetailViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            CancelCommand = new DelegateCommand(OnCancel);
            SaveCommand = new DelegateCommand(OnSave);
        }

        public ICommand CancelCommand { get; }

        public ICommand SaveCommand { get; }

        public GroceryList GroceryList
        {
            get => _groceryList;
            set => SetProperty(ref _groceryList, value);
        }

        private async void OnCancel()
        {
            var answer = await _dialogService.DisplayAlertAsync(
                             "Cancellation",
                             "You have unsaved changes, cancel anyway?",
                             "YES",
                             "NO");

            if (answer == false) return;

            await _navigationService.GoBackAsync();
        }

        private async void OnSave()
        {
            await _dialogService.DisplayAlertAsync(string.Empty, "Your grocery list was saved!", "OK");

            await _navigationService.GoBackAsync(
                new NavigationParameters { { "GroceryList", GroceryList } });
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            GroceryList = parameters["GroceryList"] as GroceryList;
        }
    }
}