using System.Threading.Tasks;

using Rg.Plugins.Popup.Pages;

using Xamarin.Forms;

namespace ShoppingList.Shared.Views
{
    public partial class AddSharedListUserPopup : PopupPage
    {
        public AddSharedListUserPopup()
        {
            InitializeComponent();
        }

        // Method for animation child in PopupPage
        // Invoked after custom animation end
        protected override Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(0.7);
        }

        // Method for animation child in PopupPage
        // Invoked before custom animation begin
        protected override Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1);
        }

        protected override bool OnBackButtonPressed()
        {
            // Prevent hide popup
            //return base.OnBackButtonPressed();
            return true;
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return default value - CloseWhenBackgroundIsClicked
            return base.OnBackgroundClicked();
        }
    }
}
