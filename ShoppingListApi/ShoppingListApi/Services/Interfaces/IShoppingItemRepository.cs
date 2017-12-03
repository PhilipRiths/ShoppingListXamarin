using ShoppingListApi.Entities;
using System;
using System.Collections.Generic;

namespace ShoppingListApi.Services
{
    public interface IShoppingItemRepository
    {
        IEnumerable<ShoppingListItem> GetShoppingListItem(Guid shoppingListId);

        bool ShoppingListExists(Guid shoppingListId);

        IEnumerable<ShoppingListItem> GetAllShoppingListItems();
    }
}