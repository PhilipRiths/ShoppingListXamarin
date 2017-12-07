using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

using Acr.UserDialogs;

using Prism.Commands;
using Prism.Services;

using ShoppingList.Shared.Helpers;
using ShoppingList.Shared.Wrappers;

namespace ShoppingList.Shared.ViewModels
{
    public class UserProfileViewModel : BaseViewModel, IAsyncInitialization
    {
        //private const string EmailRegex =
            //@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))"
            //+ @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        private readonly IPageDialogService _dialogService;
        private UserWrapper _userWrapper;

        public UserProfileViewModel(IPageDialogService dialogService)
        {
            _dialogService = dialogService;
            Initialization = InitializeAsync();

            OpenEditCommand = new DelegateCommand<string>(OnOpenEdit);
        }

        public ICommand OpenEditCommand { get; }

        public UserWrapper UserWrapper
        {
            get => _userWrapper;
            set => SetProperty(ref _userWrapper, value);
        }

        public Task Initialization { get; }

        private async Task InitializeAsync()
        {
            // TODO Get user from API
            var user = await MockUserDataStore.GetAsync(1);
            UserWrapper = new UserWrapper(user);
        }

        private async void OnOpenEdit(string commandParameterValue)
        {
            switch (commandParameterValue)
            {
                //case "email":
                    //await PromptEditEmail();
                    //break;

                case "name":
                    await PromptEditName();
                    break;
                default:
                    await _dialogService.DisplayAlertAsync("Error", "Sorry, something went wrong, error message has been sent to support.", "OK");
                    // TODO Log error
                    break;
            }
        }

        //private async Task PromptEditEmail()
        //{
        //    var result = await UserDialogs.Instance.PromptAsync(
        //                     new PromptConfig
        //                     {
        //                         Message = "Edit your email:",
        //                         CancelText = "CANCEL",
        //                         OkText = "OK",
        //                         OnTextChanged = ValidateEmail,
        //                         Text = UserWrapper.Email,
        //                         InputType = InputType.Email
        //                     });

        //    UserWrapper.Email = result.Text;
        //}

        private async Task PromptEditName()
        {
            var result = await UserDialogs.Instance.PromptAsync(
                             new PromptConfig
                             {
                                 Message = "Edit you first- and last name:",
                                 CancelText = "CANCEL",
                                 OkText = "OK",
                                 OnTextChanged = ValidateName,
                                 Text = UserWrapper.FullName,
                                 InputType = InputType.Name
                             });

            var nameParts = result.Value.Split(' ');
            UserWrapper.FirstName = nameParts[0].Trim();
            UserWrapper.LastName = nameParts[1].Trim();
        }

        //private void ValidateEmail(PromptTextChangedArgs e)
        //{
        //    e.IsValid = Regex.IsMatch(e.Value, EmailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        //}

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