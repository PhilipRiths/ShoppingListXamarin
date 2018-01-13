using Acr.UserDialogs;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

using Prism;
using Prism.Ioc;

using Rg.Plugins.Popup;

using ShoppingList.Shared;

using SimpleAuth;
using SimpleAuth.Providers;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace ShoppingList.Droid
{
    [Activity(
        Label = "ShoppingList",
        Icon = "@drawable/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Native.OnActivityResult(requestCode, resultCode, data);
        }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            Popup.Init(this, bundle);
            Google.Init(Application);

            UserDialogs.Init(this);

            LoadApplication(new App());
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}