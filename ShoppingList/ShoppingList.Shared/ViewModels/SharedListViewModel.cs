using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Prism.Commands;
using Prism.Services;

using ShoppingList.Shared.Helpers;
using ShoppingList.Shared.Wrappers;

namespace ShoppingList.Shared.ViewModels
{
    public class SharedListViewModel : BaseViewModel, IAsyncInitialization
    {
        private readonly IPageDialogService _dialogService;

        public SharedListViewModel(IPageDialogService dialogService)
        {
            _dialogService = dialogService;
            Initialization = InitializeAsync();

            AddSharedListUserCommand = new DelegateCommand(OnAddSharedListUser);
            DeleteSharedListUserCommand = new DelegateCommand(OnDeleteSharedListUser);
        }

        public DelegateCommand DeleteSharedListUserCommand { get; }

        public DelegateCommand AddSharedListUserCommand { get; }

        public ObservableCollection<UserWrapper> Users { get; private set; }

        public Task Initialization { get; }

        private async Task InitializeAsync()
        {
            Users = new ObservableCollection<UserWrapper>();

            // TODO Get users from API that are related to the shared list for the logged in user
            var users = await MockUserDataStore.GetAllAsync();

            foreach (var user in users)
            {
                Users.Add(new UserWrapper(user));
            }
        }

        private void OnAddSharedListUser()
        {
            // TODO Save user to this shared list and update UI
            _dialogService.DisplayAlertAsync(
                string.Empty,
                $"You are now sharing this list with {Users[1].FullName}",
                "OK");
        }

        private void OnDeleteSharedListUser()
        {
            // TODO Delete user from this shared list and update UI
            _dialogService.DisplayAlertAsync(
                string.Empty,
                $"User {Users[1].FullName} was successfully removed from this list",
                "OK");
        }
    }
}