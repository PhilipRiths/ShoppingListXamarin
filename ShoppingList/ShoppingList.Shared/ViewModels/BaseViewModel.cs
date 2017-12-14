using Prism.Mvvm;

using ShoppingList.Shared.Models;
using ShoppingList.Shared.Services;
using ShoppingList.Shared.Validation;

namespace ShoppingList.Shared.ViewModels
{
    public class BaseViewModel : BindableBase
    {
        private bool _isBusy;
        private string _title = string.Empty;

        public BaseViewModel()
        {
            MockShoppingListDataStore = new MockGroceryListDataStore();
            MockUserDataStore = new MockUserDataStore();
        }

        public IDataStore<GroceryList> MockShoppingListDataStore { get; }

        public IDataStore<User> MockUserDataStore { get; }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

   
    }
}