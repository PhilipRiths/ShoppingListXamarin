using ShoppingListApi.Entities;
using System;
using System.Collections.Generic;

namespace ShoppingListApi.Services
{
    public interface IShoppingListItemRepository
    {
        IEnumerable<ShoppingListItem> GetShoppingListItem(Guid shoppingListId);

        bool ShoppingListExists(Guid shoppingListId);

        IEnumerable<ShoppingListItem> GetAllShoppingListItems();
    }
}