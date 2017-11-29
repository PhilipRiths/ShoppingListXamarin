using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingList.Shared.Models;
using ShoppingList.Shared.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList.Shared.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewItemPage : ContentPage
	{
        public Items Items { get; set; }
	    public ShoppingLists ShoppingLists { get; set; }
		public NewItemPage ()
		{
			InitializeComponent ();

            Items = new Items()
            {
                Name = "Ny vara",
                InBasket = false
            };
		    BindingContext = this;
        }
	    public NewItemPage(ListDetailViewModel viewmodel)
	    {
	        InitializeComponent();

	        Items = new Items()
	        {
	            Name = "Ny vara",
	            InBasket = false
	        };
	        BindingContext = this;
	    }
        async void Save_Clicked(object sender, EventArgs e)
	    {
	        MessagingCenter.Send(this, "AddItem", Items);
	        await Navigation.PopToRootAsync();
	    }
    }
}