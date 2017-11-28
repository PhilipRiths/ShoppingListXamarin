using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;

namespace ShoppingList.Shared.ViewModels
{
    public class ShoppingListViewModel : BindableBase
    {
        public ShoppingListViewModel()
        {
            AddShoppingListCommand = new DelegateCommand(AddShoppingListExecute);
        }

        public ICommand AddShoppingListCommand { get; }

        private void AddShoppingListExecute()
        {
            throw new System.NotImplementedException();
        }
    }
}