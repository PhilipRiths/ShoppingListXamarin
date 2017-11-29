using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingList.Shared.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList.Shared.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewListPage : ContentPage
	{
        public ShoppingLists ShoppingLists { get; set; }
		public NewListPage ()
		{
			InitializeComponent ();
		}
	    async void Save_Clicked(object sender, EventArgs e)
	    {
	        MessagingCenter.Send(this, "AddList", ShoppingLists);
	        await Navigation.PopToRootAsync();
	    }
    }
}