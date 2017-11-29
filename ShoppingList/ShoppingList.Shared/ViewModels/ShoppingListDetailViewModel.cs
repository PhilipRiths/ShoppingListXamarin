using System.Threading.Tasks;
using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace ShoppingList.Shared.ViewModels
{
    public class ShoppingListDetailViewModel : BindableBase
    {
        private readonly IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public ShoppingListDetailViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            CancelCommand = new DelegateCommand(OnCancel);
            SaveCommand = new DelegateCommand(OnSave, CanSave);
        }

        public ICommand CancelCommand { get; }

        public ICommand SaveCommand { get; }

        private string _shoppingListName;

        public string ShoppingListName
        {
            get => _shoppingListName;
            set
            {
                SetProperty(ref _shoppingListName, value);
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            } 
        }


        private async void OnCancel()
        {
            await _navigationService.GoBackAsync();
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(ShoppingListName);
        }

        private async void OnSave()
        {
            await _dialogService.DisplayAlertAsync("Save", "Your shopping list was saved...", "OK");

            // TODO Push changes to API
            await _navigationService.GoBackAsync();
        }
    }
}