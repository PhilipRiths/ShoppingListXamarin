using System.Windows.Input;

using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ShoppingList.Shared.ViewModels
{
    public class ShoppingListDetailViewModel
    {
        private readonly IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public ShoppingListDetailViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            CancelCommand = new DelegateCommand(CancelCommandExecute);
            SaveCommand = new DelegateCommand(SaveCommandExecute);
        }

        public ICommand CancelCommand { get; }

        public ICommand SaveCommand { get; }

        private void CancelCommandExecute()
        {
            _navigationService.GoBackAsync();
        }

        private void SaveCommandExecute()
        {
            _dialogService.DisplayAlertAsync("Save", "Your shopping list was saved...", "OK");

            // TODO Push changes to API

            _navigationService.GoBackAsync();
        }
    }
}