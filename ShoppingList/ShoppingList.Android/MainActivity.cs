using Acr.UserDialogs;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

using Autofac;

using Prism;
using Prism.Autofac;
using Prism.Ioc;

using ShoppingList.Shared;

namespace ShoppingList.Droid
{
    [Activity(
        Label = "ShoppingList",
        Icon = "@drawable/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            SimpleAuth.Providers.Google.Init(this.Application);
            UserDialogs.Init(this);

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            SimpleAuth.Native.OnActivityResult(requestCode, resultCode, data);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}