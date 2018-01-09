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

        bool Save();
    }
}