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

	    public LoginPage(IEventAggregator eventAggregator)
	    {
	        _eventAggregator = eventAggregator;
	        InitializeComponent();
	    }

	    protected override void OnAppearing()
	    {
            _eventAggregator.GetEvent<OnStartLoginEvent>().Publish();
        }
	}
}