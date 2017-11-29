using ShoppingListApi.Entities;
using System.Collections.Generic;

namespace ShoppingListApi.Services
{
    public interface IShoppingListRepository
    {
        void AddShoppingList(ShoppingList shoppingList);

        IEnumerable<ShoppingList> GetShoppingLists();
    }
}