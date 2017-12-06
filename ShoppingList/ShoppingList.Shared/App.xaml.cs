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
