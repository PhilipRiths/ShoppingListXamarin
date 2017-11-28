using Prism.Autofac;

using ShoppingList.Shared.ViewModels;
using ShoppingList.Shared.Views;

using Xamarin.Forms;

namespace ShoppingList.Shared
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(ShoppingListPage)}");
        }

        protected override void RegisterTypes()
        {
            Builder.RegisterTypeForNavigation<NavigationPage>();
            Builder.RegisterTypeForNavigation<MainPage>();
            Builder.RegisterTypeForNavigation<ShoppingListPage, ShoppingListViewModel>();
        }
    }
}