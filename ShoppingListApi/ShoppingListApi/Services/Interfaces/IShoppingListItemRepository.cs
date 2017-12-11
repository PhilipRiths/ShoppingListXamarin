using ShoppingListApi.Entities;
using System;
using System.Collections.Generic;

namespace ShoppingListApi.Services
{
    public interface IShoppingListItemRepository
    {
        IEnumerable<ShoppingListItem> GetShoppingListsAndItemsByListId(Guid shoppingListId);

        IEnumerable<ShoppingListItem> GetShoppingListsAndItemsByItemId(Guid shoppingItemId);

        bool ShoppingListExists(Guid shoppingListId);

        bool ShoppingItemExists(Guid shoppingItemId);

        IEnumerable<ShoppingListItem> GetAllShoppingListsAndItems();

        bool Save();
    }
}