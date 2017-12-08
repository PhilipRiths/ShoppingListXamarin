using Autofac;
using Prism.Autofac;
using ShoppingList.Shared.Services;
using ShoppingList.Shared.ViewModels;
using ShoppingList.Shared.Views;

namespace ShoppingList.Shared
{
    using Xamarin.Forms;

    public partial class App : PrismApplication
    {


        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
            
        }


        protected override void OnInitialized()
        {
            InitializeComponent();
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

            Builder.RegisterType<GoogleAuthService>().As<IGoogleAuthService>().SingleInstance();
        }
    }
}
