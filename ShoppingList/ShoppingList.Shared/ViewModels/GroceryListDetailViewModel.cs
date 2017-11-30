using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace ShoppingList.Shared.ViewModels
{
    public class GroceryListDetailViewModel : BaseViewModel
    {
        private readonly IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;

        private string _shoppingListName;

        public GroceryListDetailViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            CancelCommand = new DelegateCommand(OnCancel);
            SaveCommand = new DelegateCommand(OnSave, CanSave);
        }

        public ICommand CancelCommand { get; }

        public ICommand SaveCommand { get; }

        public string ShoppingListName
        {
            get => _shoppingListName;
            set
            {
                SetProperty(ref _shoppingListName, value);
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(ShoppingListName);
        }

        private async void OnCancel()
        {
            if (!string.IsNullOrWhiteSpace(ShoppingListName))
            {
                var answer = await _dialogService.DisplayAlertAsync(
                    "Cancellation",
                    "You have unsaved changes, cancel anyway?",
                    "YES",
                    "NO");

                if (answer == false) return;
            }

            await _navigationService.GoBackAsync();
        }

        private async void OnSave()
        {
            await _dialogService.DisplayAlertAsync("Save", "Your shopping list was saved...", "OK");

            // TODO Push changes to API
            await _navigationService.GoBackAsync();
        }
    }
}