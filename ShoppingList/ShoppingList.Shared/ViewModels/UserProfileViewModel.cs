using System;
using System.Text.RegularExpressions;
using System.Windows.Input;

using Acr.UserDialogs;

using Prism.Commands;

namespace ShoppingList.Shared.ViewModels
{
    public class UserProfileViewModel : BaseViewModel
    {
        private const string EmailRegex =
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))"
            + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        private string _email;

        public UserProfileViewModel()
        {
            _email = "alem.putes@gmail.com";
            OpenEditCommand = new DelegateCommand<string>(OnOpenEdit);
        }

        public ICommand OpenEditCommand { get; }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private async void OnOpenEdit(string value)
        {
            var result = await UserDialogs.Instance.PromptAsync(
                             new PromptConfig
                             {
                                 CancelText = "CANCEL",
                                 OkText = "OK",
                                 OnTextChanged = ValidateEmail,
                                 Text = value,
                                 InputType = InputType.Email
                             });

            Email = result.Text;
        }

        private void ValidateEmail(PromptTextChangedArgs e)
        {
            e.IsValid = Regex.IsMatch(e.Value, EmailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
    }
}