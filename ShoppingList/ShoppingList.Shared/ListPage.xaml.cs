using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingList.Shared.Models;
using ShoppingList.Shared.ViewModels;
using ShoppingList.Shared.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList.Shared
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListPage : ContentPage
	{
	    private ShoppingListViewModel model;
		public ListPage ()
		{
		    InitializeComponent();

		    BindingContext = model = new ShoppingListViewModel();
		}
	    async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
	    {
            var shoppingList = args.SelectedItem as ShoppingLists;
            if (shoppingList == null)
                return;

            await Navigation.PushAsync(new ListDetailPage(new ListDetailViewModel(shoppingList)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }
	    protected override void OnAppearing()
	    {
	        base.OnAppearing();

	        if (model.ShoppingLists.Count == 0)
	            model.LoadShoppingListCommand.Execute(null);
	    }

    }
}