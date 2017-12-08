using ShoppingListApi.Entities;
using System;
using System.Collections.Generic;

namespace ShoppingListApi.Services
{
    public interface IShoppingListItemRepository
    {
        IEnumerable<ShoppingListItem> GetShoppingListItems(Guid shoppingListId);

        bool ShoppingListExists(Guid shoppingListId);

        IEnumerable<ShoppingListItem> GetAllShoppingListItems();

        ShoppingListItem GetShoppingListItemById(Guid id);

        bool Save();

        void EditShoppingItem(ShoppingItem shoppingItem);
    }
}