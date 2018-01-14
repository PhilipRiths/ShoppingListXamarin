using ShoppingListApi.Entities;
using System.Collections.Generic;

namespace ShoppingListApi.Services.Interfaces
{
    public interface IShoppingListItemRepository
    {
        IEnumerable<ShoppingListItem> GetShoppingListsAndItemsByListId(int shoppingListId);

        IEnumerable<ShoppingListItem> GetShoppingListsAndItemsByItemId(int shoppingItemId);

        bool ShoppingListExists(int shoppingListId);

        bool ShoppingItemExists(int shoppingItemId);

        IEnumerable<ShoppingListItem> GetAllShoppingListsAndItems();

        void AddShoppingItemForShoppingList(int shoppingListId, ShoppingItem shoppingItem);

        void AddShoppingListForShoppingItem(int shoppingItemId, ShoppingList shoppingList);

        bool Save();
    }
}