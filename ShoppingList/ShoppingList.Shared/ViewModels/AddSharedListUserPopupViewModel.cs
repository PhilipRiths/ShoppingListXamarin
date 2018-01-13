using Prism.Commands;
using Prism.Navigation;

namespace ShoppingList.Shared.ViewModels
{
    public class AddSharedListUserPopupViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public AddSharedListUserPopupViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            CancelCommand = new DelegateCommand(() => _navigationService.GoBackAsync());
            SaveCommannd = new DelegateCommand<string>(OnSave);
        }

        public DelegateCommand CancelCommand { get; }

        public DelegateCommand<string> SaveCommannd { get; }

        private async void OnSave(string email)
        {
            var navigationParameter = new NavigationParameters { { "Email", email } };

            await _navigationService.GoBackAsync(navigationParameter);
        }
    }
}