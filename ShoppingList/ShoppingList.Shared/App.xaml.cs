using Acr.UserDialogs;

using Autofac;

using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

using Prism.Autofac;
using Prism.Events;
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
            NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(UserProfilePage)}");
        }

        protected override void RegisterTypes()
        {
            Builder.RegisterTypeForNavigation<NavigationPage>();
            Builder.RegisterTypeForNavigation<UserProfilePage, UserProfileViewModel>();
            Builder.RegisterTypeForNavigation<GroceryListPage, GroceryListViewModel>();
            Builder.RegisterTypeForNavigation<GroceryListDetailPage, GroceryListDetailViewModel>();
            Builder.RegisterTypeForNavigation<GroceryItemPage, GroceryItemViewModel>();
            Builder.RegisterTypeForNavigation<GroceryItemDetailPage, GroceryItemDetailViewModel>();
            Builder.RegisterTypeForNavigation<LoginPage, LoginViewModel>();


            Builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            Builder.RegisterType<GoogleAuthService>().As<IGoogleAuthService>().SingleInstance();

            var connectivityInstance = CrossConnectivity.Current;
            Builder.RegisterInstance(connectivityInstance).As<IConnectivity>();

            var userDialogsInstance = UserDialogs.Instance;
            Builder.RegisterInstance(userDialogsInstance).As<IUserDialogs>();
        }
    }
}