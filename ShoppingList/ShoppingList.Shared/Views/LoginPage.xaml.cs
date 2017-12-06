using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using ShoppingList.Shared.Events;
using ShoppingList.Shared.Models;
using ShoppingList.Shared.ViewModels;
using SimpleAuth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList.Shared.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{

	    public LoginPage()
	    {
	        InitializeComponent();
	    }

	    protected override void OnAppearing()
	    {

	    }
	}
}