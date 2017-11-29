using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using ShoppingList.Shared.Models;
using Xamarin.Forms;

namespace ShoppingList.Shared.ViewModels
{
    public class ShoppingListViewModel : BaseViewModel
    {
        public ObservableCollection<ShoppingLists> ShoppingLists { get; set; }
        public ICommand LoadShoppingListCommand { get; set; }

        public ShoppingListViewModel()
        {
            Title = "Shoppinglists";
            ShoppingLists = new ObservableCollection<ShoppingLists>();
            LoadShoppingListCommand = new Command(async () => await ExecuteLoadShoppingListCommand());
            AddShoppingListCommand = new DelegateCommand(AddShoppingListExecute);
        }

        public ICommand AddShoppingListCommand { get; }

        private void AddShoppingListExecute()
        {
            throw new System.NotImplementedException();
        }
        async Task ExecuteLoadShoppingListCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ShoppingLists.Clear();
                var shoppingLists = await DataStore.GetAllAsync(true);
                foreach (var list in shoppingLists)
                {
                    ShoppingLists.Add(list);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
