using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

using ShoppingList.Shared.Models;
using ShoppingList.Shared.Services;

namespace ShoppingList.Shared.ViewModels
{
    public class GroceryListDetailViewModel : BaseViewModel
    {
        private readonly IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private string _groceryListName;

        public GroceryListDetailViewModel(
            INavigationService navigationService, 
            IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            CancelCommand = new DelegateCommand(OnCancel);
            SaveCommand = new DelegateCommand(OnSave);
        }

        public ICommand CancelCommand { get; }

        public ICommand SaveCommand { get; }

        public string GroceryListName
        {
            get => _groceryListName;
            set => SetProperty(ref _groceryListName, value);
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
            await _dialogService.DisplayAlertAsync(string.Empty, "Your shopping list was saved...", "OK");

            // TODO Push changes to API
            await _navigationService.GoBackAsync();
        }
    }
}