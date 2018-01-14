using System.Linq;

using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

using ShoppingList.Shared.Models;

namespace ShoppingList.Shared.ViewModels
{
    public class AddSharedListUserPopupViewModel : BaseViewModel, INavigatingAware
    {
        private readonly IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public AddSharedListUserPopupViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            CancelCommand = new DelegateCommand(() => _navigationService.GoBackAsync());
            SaveCommand = new DelegateCommand<string>(OnSave);
        }

        public GroceryList SelectedGroceryList { get; set; }

        public DelegateCommand CancelCommand { get; }

        public DelegateCommand<string> SaveCommand { get; }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters["SelectedGroceryList"] is GroceryList selectedGroceryList)
            {
                SelectedGroceryList = selectedGroceryList;
            }
        }

        private async void OnSave(string newSharedListUserEmail)
        {
            var user = await MockUserDataStore.GetByEmailAsync(newSharedListUserEmail);

            if (user == null)
            {
                await _dialogService.DisplayAlertAsync(
                    "ERROR",
                    $"User with email {newSharedListUserEmail} cannot be found, consider registering first.",
                    "OK");
            }
            else if (SelectedGroceryList.Users.Exists(u => u.Email == newSharedListUserEmail))
            {
                await _dialogService.DisplayAlertAsync(
                    "ERROR",
                    $"You are already sharing this list with {user.FirstName} {user.LastName}.",
                    "OK");
            }
            else
            {
                await _dialogService.DisplayAlertAsync(
                    string.Empty,
                    $"You are now sharing this list with {user.FirstName} {user.LastName}.",
                    "OK");

                var navigationParameter = new NavigationParameters { { "NewSharedListUser", user } };
                await _navigationService.GoBackAsync(navigationParameter);
            }
        }
    }
}