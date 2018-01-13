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
            DeleteSharedListUserCommand = new DelegateCommand<UserWrapper>(OnDeleteSharedListUser);
        }

        public DelegateCommand<UserWrapper> DeleteSharedListUserCommand { get; }

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

        private async void OnAddSharedListUser()
        {
            // TODO Save user to this shared list and update UI
            await _dialogService.DisplayAlertAsync(
                string.Empty,
                $"You are now sharing this list with {Users[1].FullName}.",
                "OK");

            // TODO open new window too add user.
        }

        private async void OnDeleteSharedListUser(UserWrapper selectedUser)
        {
            // TODO Implement API instead of Mock
            Users.Remove(selectedUser);

            await _dialogService.DisplayAlertAsync(
                string.Empty,
                $"You are no longer sharing this list with {selectedUser.FullName}.",
                "OK");

            await MockUserDataStore.DeleteUserByEmailAsync(selectedUser.Email);
        }
    }
}