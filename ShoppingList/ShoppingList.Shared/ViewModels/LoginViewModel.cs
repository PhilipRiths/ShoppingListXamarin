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
using ShoppingList.Shared.Views;
using SimpleAuth;
using SimpleAuth.Providers;
using Xamarin.Forms;

namespace ShoppingList.Shared.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IPageDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private GoogleApi _api;

#if __ANDROID__
			private string GoogleClientId = "707171298949-c27i7ehhs1ectmkifma6fbuft677bie9.apps.googleusercontent.com";
			private string GoogleSecret = GoogleApi.NativeClientSecret; //"041h67ZTZOryqEbNKzDkaRms";
#else
        private string GoogleClientId = "707171298949-jbjeae1jtunvpal1gtc8v7ta37b5iq9s.apps.googleusercontent.com";
        private string GoogleSecret = "041h67ZTZOryqEbNKzDkaRms";
#endif
        public LoginViewModel(INavigationService navigationService,
            IPageDialogService dialogService, IEventAggregator eventAggregator)
        {
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;

            _api = CreateApi();
            LoginCommand = new DelegateCommand(OnLoginExecute);

            MessagingCenter.Subscribe<LoginPage>(this, "LoginAppeared", (sender) =>
            {
                OnLoginExecute();
            });


        }


        public ICommand LoginCommand { get; }


        private GoogleApi CreateApi()
        {
            var scopes = new[]
            {
                "https://www.googleapis.com/auth/userinfo.email",
                "https://www.googleapis.com/auth/userinfo.profile"
            };

            var api = new GoogleApi("google",
                GoogleClientId, GoogleSecret)
            {
                Scopes = scopes,
            };
            return api;
        }

        private async void OnLoginExecute()
        {
            try
            {
                if (!_api.HasAuthenticated)
                {
                    await _api.Authenticate();

                    var profile = await _api.Get<GoogleProfile>("https://www.googleapis.com/plus/v1/people/me");
                    var navigationParameters = new NavigationParameters { { "GoogleProfile", profile } };
                    await _navigationService.NavigateAsync(nameof(GroceryListPage), navigationParameters);
                }
                else
                {
                    return;
                }
            }
            catch (TaskCanceledException e)
            {
                Debug.Write(e.Message);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }
    }
}
