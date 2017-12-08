using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using ShoppingList.Shared.Events;
using ShoppingList.Shared.Models;
using ShoppingList.Shared.Services;
using ShoppingList.Shared.Views;
using SimpleAuth;
using SimpleAuth.Providers;
using Xamarin.Forms;

namespace ShoppingList.Shared.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IPageDialogService _dialogService;
        private readonly IGoogleAuthService _googleAuthService;
        private readonly INavigationService _navigationService;

        public LoginViewModel(INavigationService navigationService,
            IPageDialogService dialogService, IGoogleAuthService googleAuthService)
        {
            _dialogService = dialogService;
            _googleAuthService = googleAuthService;
            _navigationService = navigationService;

            LoginCommand = new DelegateCommand(TrySignIn);

            MessagingCenter.Subscribe<LoginPage>(this, "LoginAppeared", (sender) =>
            {
                TrySignIn();
            });
            
        }

        public ICommand LoginCommand { get; }
        
        private async void TrySignIn()
        {
            var loggedIn = await _googleAuthService.TrySignIn();
            if (loggedIn)
            {
                await _navigationService.NavigateAsync(nameof(GroceryListPage));
            }
            else
            {
                await _dialogService.DisplayAlertAsync("Error", "Could not sign in.", "OK");
            }
        }
    }
}
