using System.Threading.Tasks;

using Prism.Commands;
using Prism.Navigation;

using ShoppingList.Shared.Helpers;

namespace ShoppingList.Shared.ViewModels
{
    public class AddSharedListUserPopupViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public AddSharedListUserPopupViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            CancelCommand = new DelegateCommand(() => _navigationService.GoBackAsync());
        }

        public DelegateCommand CancelCommand { get; }

        public string Email { get; set; }
    }
}