﻿using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using ShoppingList.Shared.Events;
using ShoppingList.Shared.Services;
using ShoppingList.Shared.Views;
using Xamarin.Forms;

namespace ShoppingList.Shared.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IPageDialogService _dialogService;
        private readonly IGoogleAuthService _googleAuthService;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;

        public LoginViewModel(INavigationService navigationService, IEventAggregator eventAggregator,
            IPageDialogService dialogService, IGoogleAuthService googleAuthService)
        {
            _dialogService = dialogService;
            _googleAuthService = googleAuthService;
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;

            LoginCommand = new DelegateCommand(TrySignIn);

            _eventAggregator.GetEvent<OnStartLoginEvent>().Subscribe(TrySignIn);
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
