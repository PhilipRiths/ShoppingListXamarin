using System;
using System.Threading.Tasks;
using Autofac;
using Prism.Autofac;
using Prism.Events;
using ShoppingList.Shared.Events;
using ShoppingList.Shared.Models;
using ShoppingList.Shared.Services;
using ShoppingList.Shared.ViewModels;
using ShoppingList.Shared.Views;
using SimpleAuth;
using SimpleAuth.Providers;

namespace ShoppingList.Shared
{
    using Xamarin.Forms;

    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
#if __ANDROID__
			string GoogleClientId = "707171298949-c27i7ehhs1ectmkifma6fbuft677bie9.apps.googleusercontent.com";
			string GoogleSecret = GoogleApi.NativeClientSecret; //"041h67ZTZOryqEbNKzDkaRms";
#else
            string GoogleClientId = "707171298949-jbjeae1jtunvpal1gtc8v7ta37b5iq9s.apps.googleusercontent.com";
            string GoogleSecret = "041h67ZTZOryqEbNKzDkaRms";
#endif
            // The root page of your application
            MainPage = new NavigationPage(new LoginPage());
        }
        
        protected override void OnInitialized()
        {
            InitializeComponent();

            // Set the page you are working with:
            NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(LoginPage)}");
        }

        protected override void RegisterTypes()
        {
            Builder.RegisterTypeForNavigation<NavigationPage>();
            Builder.RegisterTypeForNavigation<GroceryListPage, GroceryListViewModel>();
            Builder.RegisterTypeForNavigation<GroceryListDetailPage, GroceryListDetailViewModel>();
            Builder.RegisterTypeForNavigation<GroceryItemPage, GroceryItemViewModel>();
            Builder.RegisterTypeForNavigation<GroceryItemDetailPage, GroceryItemDetailViewModel>();
            Builder.RegisterTypeForNavigation<LoginPage, LoginViewModel>();
        }
    }
}
