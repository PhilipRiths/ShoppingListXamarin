using System;
using System.Collections.Generic;
using ShoppingListApi.Entities;

namespace ShoppingListApi.Services
{
    public interface IShoppingItemRepository
    {
        IEnumerable<ShoppingListItem> GetShoppingListItem(Guid shoppingListId);
    }
}