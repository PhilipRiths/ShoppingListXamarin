using ShoppingListApi.Entities;
using System;
using System.Collections.Generic;

namespace ShoppingListApi.Services.Interfaces
{
    public interface IShoppingItemRepository
    {
        void DeleteShoppingItem(ShoppingItem shoppingItem);

        void DeleteShoppingListItemContainingShoppingItem(Guid shoppingItemId);

        IEnumerable<ShoppingItem> GetAllShoppingItems();

        bool Save();

        void EditShoppingItem(ShoppingItem shoppingItem);

        void AddShoppingItem(ShoppingItem shoppingItem);

        ShoppingItem GetShoppingItem(Guid id);
    }
}