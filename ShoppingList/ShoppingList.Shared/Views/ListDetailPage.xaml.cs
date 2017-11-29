using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingList.Shared.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList.Shared.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListDetailPage : ContentPage
	{
	    private ListDetailViewModel viewModel;
		public ListDetailPage ()
		{
			InitializeComponent ();
		}

	    public ListDetailPage(ListDetailViewModel viewModel)
	    {
	            InitializeComponent();
	        BindingContext = this.viewModel = viewModel;
        }
	    async void AddItem_Clicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new NewItemPage());
	    }
    }
}