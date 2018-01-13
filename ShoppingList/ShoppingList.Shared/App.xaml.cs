using Acr.UserDialogs;

using Plugin.Connectivity;

using Prism;
using Prism.Autofac;
using Prism.Events;
using Prism.Ioc;
using Prism.Plugin.Popups;

using ShoppingList.Shared.Services;
using ShoppingList.Shared.ViewModels;
using ShoppingList.Shared.Views;

namespace ShoppingList.Shared
{
    using Xamarin.Forms;

    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null)
            : base(initializer)
        {
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            // Set the page you are working with:
            NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SharedListPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupNavigationService();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<UserProfilePage, UserProfileViewModel>();
            containerRegistry.RegisterForNavigation<GroceryListPage, GroceryListViewModel>();
            containerRegistry.RegisterForNavigation<GroceryListDetailPage, GroceryListDetailViewModel>();
            containerRegistry.RegisterForNavigation<GroceryItemPage, GroceryItemViewModel>();
            containerRegistry.RegisterForNavigation<GroceryItemDetailPage, GroceryItemDetailViewModel>();
            containerRegistry.RegisterForNavigation<SharedListPage, SharedListViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();

            containerRegistry.RegisterForNavigation<AddSharedListUserPopup, AddSharedListUserPopupViewModel>();

            containerRegistry.RegisterSingleton<IEventAggregator, EventAggregator>();
            containerRegistry.RegisterSingleton<IGoogleAuthService, GoogleAuthService>();

            var connectivityInstance = CrossConnectivity.Current;
            containerRegistry.RegisterInstance(connectivityInstance);

            var userDialogsInstance = UserDialogs.Instance;
            containerRegistry.RegisterInstance(userDialogsInstance);
        }
    }
}