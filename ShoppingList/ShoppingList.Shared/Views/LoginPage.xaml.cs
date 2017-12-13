using Prism.Events;
using ShoppingList.Shared.Events;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList.Shared.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly IEventAggregator _eventAggregator;
        private static int _instanceCounter = 0;

        public LoginPage(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if(_instanceCounter == 0)
            _eventAggregator.GetEvent<OnStartLoginEvent>().Publish();
            _instanceCounter++;
        }

    }
}