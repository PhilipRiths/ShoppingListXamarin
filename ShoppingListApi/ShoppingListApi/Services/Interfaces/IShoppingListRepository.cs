using ShoppingListApi.Entities;
using System.Collections.Generic;

namespace ShoppingListApi.Services.Interfaces
{
    public interface IShoppingListRepository
    {
        void AddShoppingList(ShoppingList shoppingList);

        IEnumerable<ShoppingList> GetShoppingLists();

        ShoppingList GetShoppingList(int Id);

        bool ShoppingListExists(int shoppingListId);

        void EditShoppingList(ShoppingList shoppingList);

        bool Save();

        void DeleteShoppingList(ShoppingList shoppingListFromRepo);

        void DeleteShoppingListItemContainingShoppingList(int shoppingListId);
    }
}