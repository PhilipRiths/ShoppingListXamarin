using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Acr.UserDialogs;

using Prism.Commands;
using Prism.Events;
using Prism.Services;

using ShoppingList.Shared.Events;
using ShoppingList.Shared.Helpers;
using ShoppingList.Shared.Wrappers;

namespace ShoppingList.Shared.ViewModels
{
    public class UserProfileViewModel : BaseViewModel, IAsyncInitialization
    {
        private readonly IPageDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IUserDialogs _userDialogs;

        public UserProfileViewModel(
            IEventAggregator eventAggregator,
            IPageDialogService dialogService,
            IUserDialogs userDialogs)
        {
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
            _userDialogs = userDialogs;
            Initialization = InitializeAsync();

            OpenEditCommand = new DelegateCommand<string>(OnOpenEdit);
            UserNotificationPreferenceChangedCommand = new DelegateCommand<object>(OnUserNotificationPreferenceChanged);
        }

        public UserWrapper UserWrapper { get; private set; }

        public ICommand UserNotificationPreferenceChangedCommand { get; }

        public ICommand OpenEditCommand { get; }

        public Task Initialization { get; }

        private async Task InitializeAsync()
        {
            // TODO Get user from API
            var user = await MockUserDataStore.GetAsync(1);
            UserWrapper = new UserWrapper(user);
        }

        private void OnUserNotificationPreferenceChanged(object notificationType)
        {
            // TODO Update the API
            var notification = (NotificationType)notificationType;

            switch (notification)
            {
                case NotificationType.GroceryItemAdded:
                    _eventAggregator.GetEvent<UserNotificationPreferenceChangedEvent>()
                        .Publish(
                            new UserNotificationPreferenceChangedEventArgs
                            {
                                NotificationType = NotificationType.GroceryItemUpdated
                            });
                    break;
                case NotificationType.GroceryItemUpdated:
                    _eventAggregator.GetEvent<UserNotificationPreferenceChangedEvent>()
                        .Publish(
                            new UserNotificationPreferenceChangedEventArgs
                            {
                                NotificationType = NotificationType.GroceryItemAdded
                            });
                    break;
                case NotificationType.GroceryItemDeleted:
                    _eventAggregator.GetEvent<UserNotificationPreferenceChangedEvent>()
                        .Publish(
                            new UserNotificationPreferenceChangedEventArgs
                            {
                                NotificationType = NotificationType.GroceryItemAdded
                            });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async void OnOpenEdit(string commandParameterValue)
        {
            if (commandParameterValue.Length <= 0) return;

            switch (commandParameterValue)
            {
                case "name":
                    await PromptEditName();
                    break;

                default:
                    await _dialogService.DisplayAlertAsync(
                        "Error",
                        "Sorry, something went wrong, error message has been sent to support.",
                        "OK");

                    // TODO Log error
                    break;
            }
        }

        private async Task PromptEditName()
        {
            var result = await _userDialogs.PromptAsync(
                             new PromptConfig
                             {
                                 Message = "Edit you first- and last name:",
                                 CancelText = "CANCEL",
                                 OkText = "OK",
                                 OnTextChanged = ValidateName,
                                 Text = UserWrapper.FullName,
                                 InputType = InputType.Name
                             });

            if (result.Ok)
            {
                var nameParts = result.Value.Split(' ');
                UserWrapper.FirstName = nameParts[0].Trim();
                UserWrapper.LastName = nameParts[1].Trim();
            }
        }

        private void ValidateName(PromptTextChangedArgs e)
        {
            var nameParts = e.Value.Split(' ');

            if (nameParts.Any(string.IsNullOrWhiteSpace) || nameParts.Length != 2)
            {
                e.IsValid = false;
            }
        }
    }
}