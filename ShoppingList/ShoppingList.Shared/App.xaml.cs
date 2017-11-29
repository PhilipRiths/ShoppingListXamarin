using Prism.Autofac;

namespace ShoppingList.Shared
{
    using Xamarin.Forms;

    public partial class App : PrismApplication
    {
        public App()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                MainPage = new MainPageTabbed();
            else
                MainPage = new NavigationPage(new MainPageTabbed());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnInitialized()
        {
          
        }

        protected override void RegisterTypes()
        {
            
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
